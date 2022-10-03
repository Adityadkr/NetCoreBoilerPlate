using CommonEntities.Enums.Project;
using CommonEntities.Helpers;
using CommonEntities.Models.ApiModels;
using CommonEntities.Services.IRepository;
using DbEntities;
using DbEntities.Models.MongoModels;
using DbEntities.Models.MongoModels.RequestModels;
using DbServices.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Helpers;
using static CommonEntities.Enums.Api.ApiCommonCode;
using static CommonEntities.Enums.Project.Roles;

namespace AppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IJwtService _jwtService;
        private readonly IAccount _accountService;
        private readonly IUsers _userService;
        private readonly ICustomer _customerService;
        private IResponseHelper _responseHelper;

        public AccountController(ICustomer customerService, IUsers userService, IConfiguration config, IJwtService jwtService, IAccount accountService, IResponseHelper responseHelper)
        {
            _config = config;
            _accountService = accountService;
            _responseHelper = responseHelper;
            _jwtService = jwtService;
            _userService = userService;
            _customerService = customerService;
        }
        [HttpPost]
        [Route("login")]
        public ResponseModel<string> Login(LoginModel login)
        {

            //if (user.FirstName == "NODE")
            //{
            //    var claims = new[] {
            //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //    new Claim("FirstName", user.FirstName)
            //    };
            //    string token = _jwtService.GenerateJSONWebToken(claims);
            //    return Ok(token);
            //} 
            var message = _userService.UserExists(login);
            if (message == "exists")
            {
                var data = _accountService.GetUser(login);
                if (data.username != null)
                {
                    string token = "";
                    if (data.role == ROLES.CUSTOMER.ToString())
                    {

                        var additionalData = _customerService.GetCustomer(data.insertedId);
                        var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim("_id", data._id.ToString()),
                        new Claim("username",data.username),
                        new Claim("name",additionalData.firstname+" "+additionalData.lastname),
                        new Claim("role",data.role),
                        new Claim("email",data.email),
                        new Claim("mobile",string.IsNullOrEmpty(data.mobile)?"":data.mobile)
                        };
                        token = _jwtService.GenerateJSONWebToken(claims);
                        //token = CommonHelper.EncryptStringAes(_config.GetValue<string>("AES:Key"), token);
                    }
                    else if(data.role == ROLES.ADMIN.ToString())
                    {
                       // var additionalData = _customerService.GetCustomer(data.insertedId);
                        var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim("_id", data._id.ToString()),
                        new Claim("username",data.username),
                        //new Claim("name",additionalData.firstname+" "+additionalData.lastname),
                        new Claim("role",data.role),
                        new Claim("email",data.email),
                        new Claim("mobile",string.IsNullOrEmpty(data.mobile)?"":data.mobile)
                        };
                        token = _jwtService.GenerateJSONWebToken(claims);
                    }
                    return _responseHelper.CreateResponse<string>((int)API_CODE.Ok, "Login Successfull", API_STATUS.Success.ToString(), token);

                }
            }
            return _responseHelper.CreateResponse<string>((int)API_CODE.DataNotFound, message, API_STATUS.Success.ToString(), "");


        }
        [HttpPost]
        [Route("register")]
        public ResponseModel<UserModel> Register(RegistrationModels user)
        {

            var data = _accountService.Register(user);
            if (data._id != null)
            {
                return _responseHelper.CreateResponse<UserModel>((int)API_CODE.Ok, "User Registered Successfully.", API_STATUS.Success.ToString(), data);
            }
            return _responseHelper.CreateResponse<UserModel>((int)API_CODE.BadRequest, "Oops!! Something went wrong, please try again late.", API_STATUS.Success.ToString(), data);

        }
    }
}
