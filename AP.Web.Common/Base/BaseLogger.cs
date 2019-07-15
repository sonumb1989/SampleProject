using AP.Web.Common.Messages;
using FluentValidation.Results;

namespace AP.Web.Common.Base
{
    public class BaseLogger
    {
        protected BaseRequest Request { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseLogger"/> class.
        /// This constructure will be used for DI initiation
        /// </summary>
        public BaseLogger() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseLogger"/> class.
        /// This is development constructor
        /// </summary>
        /// <param name="request">request object</param>
        public BaseLogger(BaseRequest request)
        {
            Request = request;
        }

        public void MethodScopeEnter<TResponse>(TResponse response)
                where TResponse : BaseResponse, new()
        {
            //_logger.MethodScopeEnter(null, $"{_actionName}.{_methodName}");
        }

        public void MethodScopeWriteLogs<TResponse>(TResponse response, ValidationResult validationResult)
            where TResponse : BaseResponse, new()
        {
            //if (validationResult != null && !validationResult.IsValid)
            //{
            //  WriteValidationLogs(validationResult);
            //}
            //else
            //{
            //  WriteExceptionLogs(response);
            //}
        }
    }
}
