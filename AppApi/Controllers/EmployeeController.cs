using CommonEntities.Models.ApiModels;
using CommonEntities.Services.IRepository;
using DbEntities.Models.MongoModel;
using DbEntities.Models.MongoModels;
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
    public class EmployeeController : ControllerBase
    {

        private IResponseHelper _responseHelper;
       
        private IMaster _masterHelper;
        private ICacheService _cacheService; IEmployee _employee;
        public EmployeeController(IEmployee employee,IBranch branchService, IResponseHelper responseHelper, IMaster master, ICacheService cache)
        {
            _responseHelper = responseHelper;
            _masterHelper = master;
            _cacheService = cache;
            _employee = employee;




        }
        [HttpPost]
        [Route("addemployee")]
        public ResponseModel<UserModel> AddCustomer(EmployeeModel customer)
        {
            var result = _employee.AddEmployee(customer);
            
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
