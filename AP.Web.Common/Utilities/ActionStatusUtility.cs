using AP.Web.Common.Extensions;
using AP.Web.Common.Messages;
using System.Linq;
using System.Net;

namespace AP.Web.Common.Utilities
{
    public class ActionStatusUtility : BaseActionUtility
  {
    public ActionStatusUtility(BaseRequest request) : base(request)
    {
    }

    public HttpStatusCode GetStatusCode<TResponse>(TResponse result)
        where TResponse : BaseResponse, new()
    {
      return GetStatusFromErrors(result, false);
    }

    public HttpStatusCode GetInternalCode<TResponse>(TResponse result)
        where TResponse : BaseResponse, new()
    {
      return GetStatusFromErrors(result, true);
    }

    private static HttpStatusCode GetStatusFromErrors<TResponse>(TResponse result, bool isInternalCode)
        where TResponse : BaseResponse, new()
    {
      // When success == true ==> return status code is OK
      if (result.Success)
      {
        return HttpStatusCode.OK;
      }

      // When success == false, but there's not information of error ==> return InternalServerError
      if (result.Errors.IsNull() || !result.Errors.Any())
      {
        return HttpStatusCode.InternalServerError;
      }

      // Get the first error code from errors object
      foreach (var error in result.Errors)
      {
        // Get internal code
        if (!string.IsNullOrEmpty(error.InternalCode) && isInternalCode)
        {
          return (HttpStatusCode)int.Parse(error.InternalCode);
        }

        // Get error code
        if (!string.IsNullOrEmpty(error.ErrorCode) && error.MessageId != "M024" && !isInternalCode)
        {
          return (HttpStatusCode)int.Parse(error.ErrorCode);
        }
      }

      // Default value of status code is OK
      if (!isInternalCode)
      {
        return HttpStatusCode.OK;
      }

      // Default value of internal code is InternalServerError
      return HttpStatusCode.InternalServerError;
    }
  }
}
