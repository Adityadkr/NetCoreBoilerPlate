using DbEntities.Models.MongoModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbServices.IRepositories
{
    public interface ICustomer
    {
        CustomerModel AddCustomer(CustomerModel customer);
        List<CustomerModel> GetCustomers();
        CustomerModel GetCustomer(string id);
    }
}
