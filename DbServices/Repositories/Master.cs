using AutoMapper;
using DbEntities.Models.MongoModels;
using DbServices.Helpers;
using DbServices.IRepositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbServices.Repositories
{
    public class Master : IMaster
    {
        private readonly IConfiguration _configuration;

        private readonly MongoDBHelper _mongodBHelper;
        private readonly Mapper _mapper;

        public Master(IConfiguration configuration)
        {
            _configuration = configuration;
            _mongodBHelper = new MongoDBHelper("TripPlanner", _configuration);
        }

        public MasterCode AddMaster(MasterCode master)
        {
            try
            {
                _mongodBHelper.InsertOne<MasterCode>("masters", master);
                var data = _mongodBHelper.Get<MasterCode>("masters");
                return data[data.Count - 1];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public List<MasterCode> GetMaster(string key1, string key2 = null, string pcode = null)
        //{
        //    try
        //    {

        //        var data = _mongodBHelper.GetByKeyList<MasterCode>("masters", x => x.key1 == key1 && x.key2 == key2 && x.pcode == pcode);
        //        return data;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public List<MasterCode> GetMaster()
        {
            try
            {

                var data = _mongodBHelper.Get<MasterCode>("masters");
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
