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
    public class MastersController : ControllerBase
    {
        private IResponseHelper _responseHelper;
        private IMaster _masterHelper;
        private ICacheService _cacheService;
        public MastersController(IMaster masterHelper, IResponseHelper responseHelper, ICacheService cacheService)
        {
            _masterHelper = masterHelper;
            _responseHelper = responseHelper;
            _cacheService = cacheService;
        }

        [HttpPost]
        [Route("addmaster")]
        public ResponseModel<MasterCode> AddMaster(MasterCode master)
        {
            var result = _masterHelper.AddMaster(master);
            if (result.code == master.code)
            {
                return _responseHelper.CreateResponse<MasterCode>((int)API_CODE.Ok, "Master Inserted Successfully", API_STATUS.Success.ToString(), result);
            }
            else
            {
                return _responseHelper.CreateResponse<MasterCode>((int)API_CODE.BadRequest, "Oops!! Something went wrong.", API_STATUS.Failure.ToString(), null);

            }
        }
        [HttpGet]
        [Route("getmaster")]
        public ResponseModel<List<MasterCode>> GetMaster(string key1, string key2 = null, string pcode = null)
        {

            var result = _cacheService.GetData<List<MasterCode>>("lstMst");
            if (result == null)
            {
                result = _masterHelper.GetMaster();
                _cacheService.SetData<List<MasterCode>>("lstMst", result, DateTimeOffset.Now.AddMinutes(5.0));

            }


            if (result.Count > 0)
            {
                return _responseHelper.CreateResponse<List<MasterCode>>((int)API_CODE.Ok, "Data Found.", API_STATUS.Success.ToString(), result);
            }
            else
            {
                return _responseHelper.CreateResponse<List<MasterCode>>((int)API_CODE.BadRequest, "Data Not Found.", API_STATUS.Failure.ToString(), null);

            }
        }
    }
}
