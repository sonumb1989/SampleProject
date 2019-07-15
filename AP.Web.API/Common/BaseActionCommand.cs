using AP.Web.Common.Base;
using AP.Web.Common.Exceptions;
using AP.Web.Common.Extensions;
using AP.Web.Common.Messages;
using AP.Web.Common.Utilities;
using AP.Web.Common.Validations;
using AP.Web.Persistence.Data;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace AP.Web.API.Common
{
  public abstract class BaseActionCommand<TBusiness> : Disposable, ICommandAction, IValidateModelState
    where TBusiness : class
  {
    #region [Public Members]

    /// <summary>
    /// ValidationId
    /// </summary>
    public string ValidationId { get; set; }

    public ValidationResult ValidateResult { get; set; }

    /// <summary>
    /// MetaData 
    /// </summary>
    public BaseActionMetaData<TBusiness> MetaData { get; set; }

    /// <summary>
    /// BusinessLogic 
    /// </summary>
    public TBusiness BusinessLogic { get; set; }

    /// <summary>
    /// DataContext
    /// TODO: Create context and using all system
    /// </summary>
    public IDataContext Context { get; set; }

    /// <summary>
    /// Mapping properties from request object to BaseRequest
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <returns></returns>
    public TRequest ToRequest<TRequest>()
          where TRequest : BaseRequest, new()
    {
      var request = new TRequest()
      {
        ValidationId = ValidationId
      };

      return request;
    }

    #endregion [End Public Members]

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseActionCommand{TBusiness}"/> class. BaseActionCommand
    /// </summary>
    protected BaseActionCommand()
    {
      // Init validation Id which is used for logger
      ValidationId = ValidationIdGenerator.Gen();
    }

    #endregion Constructors

    #region [Abstract]

    /// <summary>
    /// Init data
    /// </summary>
    public abstract void Init();

    /// <summary>
    /// Validate function
    /// </summary>
    /// <returns></returns>
    public abstract bool Validate();

    /// <summary>
    /// Execute action
    /// </summary>
    /// <returns></returns>
    public abstract Task<BaseServicesResult> ExecuteAction(IDataContext context);

    #endregion [End Abstract]

    #region [Private Members]

    private string _errorToLog;

    #endregion [End Private Members]

    #region [Execute]

    /// <summary>
    /// Invokes Business Logic's method by name
    /// </summary>
    /// <typeparam name="TResponse">Method responese</typeparam>
    /// <param name="methodArgs">Parameters's method</param>
    /// <returns>Response's method</returns>
    public async Task<BaseServicesResult> Execute<TResponse>(params object[] methodArgs)
      where TResponse : BaseResponse, new()
    {
      // Init base request data
      BaseRequest request = ToRequest<BaseRequest>();

      // Init business logic and action meta data
      BusinessLogic = StartupExtensions.Resolve<TBusiness>();

      // Init Meta data
      MetaData = new BaseActionMetaData<TBusiness>(this);

      // TODO: Create context and using all system
      Context = Context;

      // Custom init from action
      Init();

      // Init action utilities and logger
      var statusUtility = new ActionStatusUtility(request);
      var actionUtility = new ActionExecutionUtility(request);

      // TODO: Start to write log
      // Init default response object (equal NULL)
      TResponse response;

      if (Validate())
      {
        response = await OnExecute<TResponse>(methodArgs);
      }
      else
      {
        response = new TResponse { Errors = actionUtility.GetErrors(ValidateResult), Success = false };
      }

      // Build and return service message
      var serviceResult = new BaseServicesResult
      {
        ReturnCode = statusUtility.GetStatusCode(response),
        ReturnData = new BaseResponseHttpActionResult<TResponse>()
        {
          Result = response,
          InternalCode = (int)statusUtility.GetInternalCode(response)
        }
      };

      return serviceResult;
    }

    /// <summary>
    /// Invokes Business Logic's method by name
    /// </summary>
    /// <typeparam name="TResponse">Method responese</typeparam>
    /// <param name="methodArgs">Parameters's method</param>
    /// <returns>Response's method</returns>
    private async Task<TResponse> OnExecute<TResponse>(params object[] methodArgs)
      where TResponse : BaseResponse, new()
    {
      TResponse response;
      try
      {
        //using (var scope = GetTransactionScope())
        //{
        dynamic awaitable = MetaData.Method.Invoke(BusinessLogic, methodArgs);
        await awaitable;
        response = (TResponse)awaitable.GetAwaiter().GetResult();

        //if (scope != null && response.Success)
        //{
        //  scope.Commit();
        //  if (scope.TransactionStatus == TransactionStatus.Aborted)
        //  {
        //    throw new TransactionAbortedException();
        //  }
        //}
        //}
      }
      catch (Exception ex)
      {
        _errorToLog = ExceptionUtilities.GetFullExceptionMessage(ex);
        return new TResponse()
        {
          Success = false,
          Errors = new ServiceErrors { ex.GetExceptionInfo() }
        };
      }

      return response;
    }

    #endregion [End Execute]
  }
}
