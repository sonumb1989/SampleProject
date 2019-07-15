using AP.Web.Common.Constants;
using AP.Web.Common.Extensions;
using AP.Web.Common.Messages;
using FluentValidation.Results;
using System.Net;
using System.Text;

namespace AP.Web.Common.Utilities
{
    public class ActionExecutionUtility : BaseActionUtility
  {
    private readonly string _validationId;

    public ActionExecutionUtility(BaseRequest request) : base(request)
    {
      _validationId = request.ValidationId;
    }

    public ServiceErrors GetErrors(ValidationResult validationResult)
    {
      var errors = new ServiceErrors();

      foreach (var item in validationResult.Errors)
      {
        ServiceError error = new ServiceError()
        {
          ErrorCode = item.ErrorCode,
          InternalCode = ((int)HttpStatusCode.BadRequest).ToString(),
          MessageId = "M024",
          FieldName = item.PropertyName,
#if DEBUG
          Message = GetErrorMessage(item)
#endif
        };

        error.AddValidationId(_validationId);
        errors.Add(error);
      }

      return errors;
    }

    private string GetErrorMessage(ValidationFailure item)
    {
      // Build message from failure item
      StringBuilder sb = new StringBuilder("{");
      sb.Append($"ValidationType=\"{KeyConstants.OtherError}\"");
      sb.Append($", PropertyName=\"{item.PropertyName}\"");
      sb.Append($", Value=\"{item.AttemptedValue ?? "Null"}\"");
      sb.Append("}");
      return sb.ToString();
    }
  }
}
