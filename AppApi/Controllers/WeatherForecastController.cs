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

namespace AppApi.Controllers
{

    [ApiController]
    // [Authorize]
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
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IDemo demo, ICacheService cache, ILogger<WeatherForecastController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _demo = demo;
            _cache = cache;
        }


        [HttpGet]
        public ResponseModel<List<User>> Get()
        {
            ResponseHelper<List<User>> helper = new ResponseHelper<List<User>>();
            try
            {
                var data = _cache.GetData<List<User>>("lstUsers");

                if (data == null)
                {
                    var result = _demo.getUsers();
                    var response = helper.CreateResponse((int)API_CODE.Ok, "Data Found", API_STATUS.Success.ToString(), result);
                    _cache.SetData<List<User>>("lstUsers", result, DateTimeOffset.Now.AddMinutes(5.0));
                    return response;
                }
                else
                {
                    var response = helper.CreateResponse((int)API_CODE.Ok, "Data Found", API_STATUS.Success.ToString(), data);
                    return response;
                }
            }
            catch (Exception ex)
            {
                return helper.HandleException(ex);
            }

        }
    }
}
