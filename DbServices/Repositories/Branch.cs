using DbEntities.Models.MongoModels;
using DbServices.Helpers;
using DbServices.IRepositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbServices.Repositories
{
    public class Branch: IBranch
    {
        private readonly IConfiguration _configuration;

        private readonly MongoDBHelper _mongodBHelper;


        public Branch(IConfiguration configuration)
        {
            _configuration = configuration;
            _mongodBHelper = new MongoDBHelper("TripPlanner", _configuration);
        }

        public BranchModel Addbranch(BranchModel branch)
        {
            try
            {
                _mongodBHelper.InsertOne<BranchModel>("branch",branch);
                var result = _mongodBHelper.Get<BranchModel>("branch");
                return result[result.Count - 1];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BranchModel> GetBranches()
        {
            try
            {
              
                return _mongodBHelper.Get<BranchModel>("branch");
                //return _mongodBHelper.GetCollection<BranchModel>("branch");
                 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
