using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace AP.Web.Common.Attributes
{
    public class AutoAttribute: TypeFilterAttribute
  {
    public AutoAttribute() : base(typeof(AutoLogActionFilterImpl))
    {
    }

    private class AutoLogActionFilterImpl : IActionFilter
    {
      private readonly ILogger _logger;
      public AutoLogActionFilterImpl(ILoggerFactory loggerFactory)
      {
        _logger = loggerFactory.CreateLogger<AutoAttribute>();
      }

      public void OnActionExecuting(ActionExecutingContext context)
      {
        // perform some business logic work
      }

      public void OnActionExecuted(ActionExecutedContext context)
      {
        //TODO: log body content and response as well
        _logger.LogDebug($"path: {context.HttpContext.Request.Path}");
      }
    }
  }
}
