using CommonEntities.Models.ApiModels;
using CommonEntities.Helpers;
using DbEntities;
using DbServices.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CommonEntities.Enums.Api.ApiCommonCode;
using CommonEntities.Services.IRepository;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using WebApp.Helpers;

namespace AppApi.Controllers
{

    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly IConfiguration _config;
        private readonly IDemo _demo;
        private readonly ICacheService _cache;
        private readonly IResponseHelper _responseHelper;
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWebHostEnvironment _host;

        public WeatherForecastController(IWebHostEnvironment host,IResponseHelper responseHelper, IDemo demo, ICacheService cache, ILogger<WeatherForecastController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _demo = demo;
            _cache = cache;
            _responseHelper = responseHelper;
            _host = host;
        }


        [HttpGet]
        public ResponseModel<List<User>> Get()
        {
            
           
                var data = _cache.GetData<List<User>>("lstUsers");

                if (data == null)
                {
                    var result = _demo.getUsers();
                    // var response = helper.CreateResponse((int)API_CODE.Ok, "Data Found", API_STATUS.Success.ToString(), result);
                     var response = _responseHelper.CreateResponse((int)API_CODE.Ok, "Data Found", API_STATUS.Success.ToString(), result);
                    _cache.SetData<List<User>>("lstUsers", result, DateTimeOffset.Now.AddMinutes(5.0));
                    return response;
                }
                else
                {
                    var response = _responseHelper.CreateResponse((int)API_CODE.Ok, "Data Found", API_STATUS.Success.ToString(), data);
                    return response;
                }
           

        }

        [HttpPost]
        [Route("UploadFile")]
        public bool UploadFile() {

            var file = HttpContext.Request.Form.Files;
            var path = _host.WebRootPath;
            var imagePath = _config.GetValue<string>("DocumentPath:Images");
            string[] type = { ".jpeg" };
            var result = CommonHelper.ValidateFile(file[0],type, 2);
            var fullPath = Path.Combine(path, imagePath);

            var document = CommonHelper.UploadFile(file[0], fullPath);
            return true;
        }

        [HttpPost]
        [Route("test")]
        public bool test()
        {

            throw new Exception();
        }
    }
}
