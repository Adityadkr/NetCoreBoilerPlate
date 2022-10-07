using CommonEntities.Models.ApiModels;
using CommonEntities.Services.IRepository;
using DbEntities.Models.MongoModels;
using DbServices.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CommonEntities.Enums.Api.ApiCommonCode;

namespace AppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private IResponseHelper _responseHelper;
        private IBranch _branchService;
        private IMaster _masterHelper;
        private ICacheService _cacheService;
        public BranchController(IBranch branchService, IResponseHelper responseHelper, IMaster master, ICacheService cache)
        {
            _responseHelper = responseHelper;
            _masterHelper = master;
            _cacheService = cache;
            _branchService = branchService;
        }
        [HttpGet]
        [Route("getbranches")]
        public async Task<ResponseModel<List<BranchModel>>> GetBranches(string country = null, string state = null, string city = null)
        {
            // var collection = _cacheService.GetData<IMongoCollection<BranchModel>>("lstBranch");
            var branchcollection = _branchService.GetBranches();
            var mastercollection = _masterHelper.GetMaster();
            var result = new List<BranchModel>();
            var  data  = _cacheService.GetData<List<BranchModel>>("lstBranch");
            if (data == null)
            {
                var result1 = (from bc in branchcollection
                               join mc in mastercollection on bc.branchcountry equals mc.code 
                               join sc in mastercollection on bc.branchstate equals sc.code
                               join cct in mastercollection on bc.branchcity equals cct.code
                               join c in mastercollection on cct.code equals null
                              
                               into joinedData
                               from d in joinedData.DefaultIfEmpty()
                               select new BranchModel
                               {
                                   branchname = bc.branchname,
                                   branchaddress = bc.branchaddress,
                                   branchcity = cct.codedescription,
                                   branchcountry = mc.codedescription,
                                   branchstate = sc.codedescription,

                                   is_admin_allocated = bc.is_admin_allocated,
                                   admin_id = bc.admin_id
                               }
                               );

                result = result1.ToList();
                _cacheService.SetData<List<BranchModel>>("lstBranch", result, DateTime.Now.AddSeconds(30));

            }
            else 
            {
                result = data;
            }



            //   var result = _cacheService.GetData<IMongoCollection<BranchModel>>("lstBranch");
            //if (result == null)
            //{
            //    result = _branchService.GetBranches();
            //    _cacheService.SetData<List<BranchModel>>("lstBranch", result, DateTimeOffset.Now.AddMinutes(1.0));
            //}
            if (result.Count > 0)
            {
                if (!string.IsNullOrEmpty(country))
                {
                    result = result.Where(x => x.branchcountry == country).ToList();
                }
                if (!string.IsNullOrEmpty(state))
                {
                    result = result.Where(x => x.branchstate == state).ToList();

                }
                if (!string.IsNullOrEmpty(city))
                {
                    result = result.Where(x => x.branchcity == city).ToList();

                }


                if (result.Count > 0) 
                {
                return _responseHelper.CreateResponse<List<BranchModel>>((int)API_CODE.Ok, "Data Found", API_STATUS.Success.ToString(), result);
                }
                return _responseHelper.CreateResponse<List<BranchModel>>((int)API_CODE.DataNotFound, "Data Not Found", API_STATUS.Failure.ToString(), null);


            }

            else
            {
                return _responseHelper.CreateResponse<List<BranchModel>>((int)API_CODE.DataNotFound, "Data Not Found", API_STATUS.Failure.ToString(), null);
            }
          

        }
        [HttpPost]
        [Route("addbranch")]
        public ResponseModel<BranchModel> AddBranch(BranchModel branch)
        {
            var result = _branchService.Addbranch(branch);
            if (result._id != null)
            {
                _cacheService.RemoveData("lstBranch");
                return _responseHelper.CreateResponse<BranchModel>((int)API_CODE.Ok, "Branch added successfully.", API_STATUS.Success.ToString(), result);

            }
            else
                return _responseHelper.CreateResponse<BranchModel>((int)API_CODE.Failure, "Oops!! Something went wrong.", API_STATUS.Failure.ToString(), null);

        }
    }
}
