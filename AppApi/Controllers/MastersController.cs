using CommonEntities.Models.ApiModels;
using CommonEntities.Services.IRepository;
using DbEntities.Models.MongoModels;
using DbEntities.Models.MongoModels.ResponseModels;
using DbServices.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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
        private IUsers _userService;
        private ICacheService _cacheService;
        public MastersController(IMaster masterHelper, IResponseHelper responseHelper, ICacheService cacheService, IUsers userService)
        {
            _masterHelper = masterHelper;
            _responseHelper = responseHelper;
            _cacheService = cacheService;
            _userService = userService;
        }

        [HttpPost]
        [Route("addmaster")]
        public ResponseModel<MasterCode> AddMaster(MasterCode master)
        {
            master._id = ObjectId.GenerateNewId();
            master.insertedId = master._id.ToString();
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
                if (!string.IsNullOrEmpty(key1))
                {
                    result = result.Where(x => x.key1 == key1).ToList();
                }
                if (!string.IsNullOrEmpty(key2))
                {

                    result = result.Where(x => x.key2 == key2).ToList();
                }
                if (!string.IsNullOrEmpty(pcode))
                {
                    result = result.Where(x => x.pcode == pcode).ToList();
                }
                return _responseHelper.CreateResponse<List<MasterCode>>((int)API_CODE.Ok, "Data Found.", API_STATUS.Success.ToString(), result);
            }
            else
            {
                return _responseHelper.CreateResponse<List<MasterCode>>((int)API_CODE.BadRequest, "Data Not Found.", API_STATUS.Failure.ToString(), null);

            }
        }

        [HttpGet]
        [Route("getusers")]
        public ResponseModel<List<DDModel>> GetUser(string role, string city = null)
        {

            var users = _userService.GetUsers(role);
            var lstrole = _masterHelper.GetMaster().Where(x => x.key1 == "ROLES");
            var result = (from u in users
                          join r in lstrole on u.role equals r.insertedId
                          where u.role == role


                          select new DDModel
                          {
                              mstkey = u.username,
                              mstvalue = u._id.ToString()
                          }
                          );

                return _responseHelper.CreateResponse<List<DDModel>>((int)API_CODE.Ok, "Data Found.", API_STATUS.Success.ToString(),result.ToList());
           


        }
    }
}
