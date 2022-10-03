using CommonEntities.Models.ApiModels;
using CommonEntities.Services.IRepository;
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
    public class UserController : ControllerBase
    {
        private IUsers _user;
        private IResponseHelper _responseHelper;
        public UserController(IUsers user, IResponseHelper responseHelper)
        {
            _user = user;
            _responseHelper = responseHelper;
        }

        [HttpPost]
        [Route("adduser")]
        public ResponseModel<UserModel> AddUser(UserModel user)
        {
            var result = new UserModel();

            result = _user.AddUser(user);
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
