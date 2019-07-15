using AP.Web.Common.Messages;
using AP.Web.Persistence.Data.Entities;
using AP.Web.Services.Models.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AP.Web.Services.Data.Products
{
    public interface IProductBusinessLogic
    {
        Task<BaseResponse<IEnumerable<Product>>> GetListProduct(ProductRequest request);

        Task<BaseResponse> InsertProduct(ProductRequest request);
    }
}
