using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace AP.Web.Common.Messages
{
  public class HttpActionResult : IActionResult
  {
    private readonly HttpRequestMessage _request;
    private readonly BaseServicesResult _result;

    public HttpActionResult(BaseServicesResult result, HttpRequestMessage request = null)
    {
      _result = result;
      _request = request;
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
      var objectResult = new ObjectResult(_result.ReturnData)
      {
        StatusCode = (int)_result.ReturnCode
      };

      await objectResult.ExecuteResultAsync(context);
    }
  }
}
