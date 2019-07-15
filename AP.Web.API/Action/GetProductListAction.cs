using AP.Web.API.Common;
using AP.Web.Common.Messages;
using AP.Web.Persistence.Data;
using AP.Web.Services.Data.Products;
using AP.Web.Services.Models.Requests;
using System.Threading.Tasks;

namespace AP.Web.API.Action
{
  public class GetListProductAction : BaseActionCommand<IProductBusinessLogic>
  {
    public ProductRequest req;

    /// <summary>
    /// Method init
    /// </summary>
    public override void Init()
    {
    }

    /// <summary>
    /// Validation
    /// </summary>
    /// <returns></returns>
    public override bool Validate()
    {
      return true;
    }

    /// <summary>
    /// ExecuteAction
    /// </summary>
    /// <returns>Result</returns>
    public async override Task<BaseServicesResult> ExecuteAction(IDataContext context)
    {
      req = ToRequest<ProductRequest>();
      return await Execute<BaseResponse>(req);
    }
  }
}
