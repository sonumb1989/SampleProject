using AP.Web.Common.Messages;
using AP.Web.Persistence.Data.Entities;

namespace AP.Web.Services.Models.Requests
{
    public class ProductRequest : BaseRequest
    {
        public string Code { get; set; }
    }
}
