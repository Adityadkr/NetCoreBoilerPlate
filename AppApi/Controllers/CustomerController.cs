using CommonEntities.Models.ApiModels;
using CommonEntities.Services.IRepository;
using DbEntities.Models.MongoModel;
using DbServices.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CommonEntities.Enums.Api.ApiCommonCode;

namespace AppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomer _customer;
        private IResponseHelper _responseHelper;
        public CustomerController(ICustomer customer, IResponseHelper responseHelper)
        {
            _customer = customer;
            _responseHelper = responseHelper;
        }

        [HttpGet]
        [Route("getcustomer")]
        public ResponseModel<List<CustomerModel>> GetCustomers()
        {                                                                       
           
           var result = _customer.GetCustomers();
            if (result.Count > 0)
            {
                return _responseHelper.CreateResponse((int)API_CODE.Ok, "Data Found", API_STATUS.Success.ToString(), result);
            }
            else 
            {
                return _responseHelper.CreateResponse((int)API_CODE.Ok, "Data Not Found", API_STATUS.Success.ToString(), result);

            }



        }
        [HttpPost]
        [Route("addcustomer")]
        public ResponseModel<CustomerModel> AddCustomer(CustomerModel customer)
        {
            var result = new CustomerModel();

            result = _customer.AddCustomer(customer);
            result.insertedId = result._id.ToString();
            if (result._id != null)
            {
                return _responseHelper.CreateResponse((int)API_CODE.Ok, "Data Inserted Successfully", API_STATUS.Success.ToString(), result);
            }
            else
            {
                return _responseHelper.CreateResponse((int)API_CODE.Failure, "Data Insertion Failed", API_STATUS.Failure.ToString(), result);

            }



        }
    }
}
