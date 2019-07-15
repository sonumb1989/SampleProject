using AP.Web.Common.Messages;
using AP.Web.Persistence.Data.Entities;
using System;
using System.Collections.Generic;

namespace AP.Web.Services.Data.Customers
{
    public interface ICustomerBusinessLogic
    {
        bool InsertCustomer(Customer cus);
        bool GetCustomer(BaseRequest request);
        Customer GetCustomerByIdentityId(string id);
        IList<Customer> GetAllCustomer();
    }
}
