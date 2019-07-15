using AP.Web.Common.Extensions;
using AP.Web.Common.Messages;
using AP.Web.Persistence.Data;
using AP.Web.Persistence.Data.Entities;
using AP.Web.Persistence.Repository;
using AP.Web.Services.Common;
using AP.Web.Services.Data.Customers;
using AP.Web.Services.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AP.Web.Services.Data.Products
{
    public class ProductBusinessLogic : BaseBusinessLogic, IProductBusinessLogic
    {
        // Init business logic and action meta data
        private ICustomerBusinessLogic _iCust;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductBusinessLogic"/> class.
        /// </summary>
        /// /// <param name="dataAccess">ICMN110051DataAccess</param>
        /// /// <param name="dataContext">IDataContext</param>
        public ProductBusinessLogic(IDataContext context, IBaseRepository<Product> repository)
          : base(context)
        {
            _iCust = StartupExtensions.Resolve<ICustomerBusinessLogic>();
        }
        public async Task<BaseResponse<IEnumerable<Product>>> GetListProduct(ProductRequest request)
        {
            //using (var biz = BizManager<ICustomerBusinessLogic>())
            //{
            //    var num = 1;
            //}
            var product = UnitOfWork.GetRepository<Product>();
            Expression<Func<Product, bool>> pre = x => x.IsActive;
            Func<IQueryable<Product>, IOrderedQueryable<Product>> order = x => x.OrderBy(y=>y.Name);
            var list = product.GetPagedList(null, null, null);
            var response = new BaseResponse<IEnumerable<Product>>
            {
                Success = true,
                Data = UnitOfWork.GetRepository<Product>().GetAll().ToList()
            };

            return await Task.FromResult(response);
        }

        public async Task<BaseResponse> InsertProduct(ProductRequest request)
        {
            var response = new BaseResponse
            {
                Success = true       
            };

            return await Task.FromResult(response);
        }

        protected override void ConfigAutoMapper()
        {
        }
    }
}
