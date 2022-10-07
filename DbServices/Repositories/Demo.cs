using AutoMapper;
using DbEntities;
using DbEntities.Models.MongoModels;
using DbServices.Helpers;
using DbServices.IRepositories;
using DbServices.Objects;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DbServices.Repositories
{
    public class Demo : IDemo
    {


        private readonly IConfiguration _configuration;
        private readonly string con;
        private readonly SqlDBHelper _dBHelper;
        private readonly MongoDBHelper _mongodBHelper;
        private readonly Mapper _mapper;
        //string ss = _configuration.GetConnectionString("defaultConnection").ToString();
        public Demo(IConfiguration configuration)
        {
            _configuration = configuration;
            _dBHelper = new SqlDBHelper(configuration);
            _mongodBHelper = new MongoDBHelper("AlienDBex",_configuration);

            con = _configuration.GetConnectionString("defaultConnection").ToString();
        }


        public List<User> getUsers()
        {

            RootObject<User> ROOT = new RootObject<User>();
            ROOT.OPERATION = "GET";
            DataTable dt = new DataTable();
            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            //GET DATATBLE FROM JSON PARAMETERS
            sqlParameters.Add(new SqlParameter("@JSON", JsonConvert.SerializeObject(ROOT)));
            dt = _dBHelper.GetDatatable(DBStoredProcedures.USP_JCRUD_USERS, CommandType.StoredProcedure, sqlParameters);


            //GET DATATABLE USING SELECT QUERY
            //sqlParameters.Add(new SqlParameter("@test", "NODE"));
            //dt = _dBHelper.GetDatatable("SELECT * FROM [USERS] WHERE FirstName = @test",CommandType.Text,sqlParameters);



            //GET DATASET USING SELECT QUERY
            // sqlParameters.Add(new SqlParameter("@test", "NODE"));
            // DataSet dS = _dBHelper.GetDataSet("SELECT * FROM [USERS] WHERE FirstName = @test",CommandType.Text,sqlParameters);
            List<User> result = JsonConvert.DeserializeObject<List<User>>(JsonConvert.SerializeObject(dt)); ;
            return result;
        }
        public List<Alien> GetAliens()
        {
            var data =  _mongodBHelper.Get<Alien>("aliens");
            return data;

        }

        public Alien GetAliensByID(string id)
        {
            try
            {
                var data =  _mongodBHelper.GetByKey<Alien>("aliens", x => x._id == ObjectId.Parse(id));
                return data;
            }
            catch (Exception ex) {
                throw ex;
            }
            

        }

        //public bool AddAlien(Alien alien)
        //{
        //    var data =  _mongodBHelper.InsertOne<Alien>("aliens",alien);
        //    return data;

        //}
        //public bool UpdateAlien(Alien alien)
        //{
        //    var data =   _mongodBHelper.UpdateOne<Alien>("aliens", alien,x=>x._id == alien._id);
        //    return data;

        //}
        //public bool DeleteAlien(Alien alien)
        //{
        //    var data =  _mongodBHelper.DeleteOne<Alien>("aliens",  x => x._id == alien._id);
        //    return data;

        //}
        public bool AddAlien(Alien alien)
        {
            alien._id = ObjectId.GenerateNewId();
        
            var data =  _mongodBHelper.InsertOne<Alien>("aliens", alien);

            if (data._id != null) 
            {
                return true;
            }
            return false;
        }
        public bool UpdateAlien(Alien alien)
        {
            var data = _mongodBHelper.UpdateOne<Alien>("aliens", alien, x => x._id == alien._id);
            return data;

        }
        public bool DeleteAlien(Alien alien)
        {
            var data =  _mongodBHelper.DeleteOne<Alien>("aliens", x => x._id == alien._id);
            return data;

        }

    }
}
