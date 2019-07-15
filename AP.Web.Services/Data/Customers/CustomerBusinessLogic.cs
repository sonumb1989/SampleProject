using AP.Web.Common.Messages;
using AP.Web.Persistence.Data;
using AP.Web.Persistence.Data.Entities;
using AP.Web.Services.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AP.Web.Services.Data.Customers
{
    public class CustomerBusinessLogic : BaseBusinessLogic, ICustomerBusinessLogic
    {
        public CustomerBusinessLogic(IDataContext context)
          : base(context)
        {
            //ConfigAutoMapper();
        }

        public IList<Customer> GetAllCustomer()
        {
            return UnitOfWork.GetRepository<Customer>(false).GetAll()        // source
                 .Join(UnitOfWork.GetRepository<AppUser>(false).GetAll(),         // target
                    cust => cust.IdentityId,          // FK
                    user => user.Id,   // PK
                    (cust, user) => new { Customer = cust, AppUser = user })
                    .Select(x => x.Customer).ToList(); // project result
        }

        public bool GetCustomer(BaseRequest request)
        {
            return true;
        }

        public Customer GetCustomerByIdentityId(string id)
        {
            return UnitOfWork.GetRepository<Customer>(hasCustomRepository: false).GetFirstOrDefault(x => x.Identity.Id == id, null
              , source => source.Include(x => x.Identity), true);
        }

        public bool HealthCheck()
        {
            return true;
        }

        public bool InsertCustomer(Customer cus)
        {
            try
            {
                UnitOfWork.GetRepository<Customer>(false).InsertAsync(cus);
                UnitOfWork.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected override void ConfigAutoMapper()
        {
        }
    }
}
