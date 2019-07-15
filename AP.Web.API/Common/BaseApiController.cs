using AP.Web.Common.Extensions;
using AP.Web.Common.Messages;
using AP.Web.Persistence.Data;
using Microsoft.AspNetCore.Mvc;

namespace AP.Web.API.Common
{
  public abstract class BaseApiController : Controller
  {
    protected BaseApiController()
    {
      DataContext = StartupExtensions.Resolve<IDataContext>();
    }

    /// <summary>
    /// DataContext
    /// </summary>
    public IDataContext DataContext { get; set; }


    protected HttpActionResult ExecuteAction<TBussiness>(BaseActionCommand<TBussiness> command)
    where TBussiness : class
    {
      var result = command.ExecuteAction(DataContext);
      return new HttpActionResult(result.Result);
    }
  }
}
