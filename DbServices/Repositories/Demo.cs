using AutoMapper;
using DbEntities;
using DbServices.Helpers;
using DbServices.IRepositories;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DbServices.Repositories
{
    public class Demo : IDemo
    {
        private readonly IConfiguration _configuration;
        private readonly string con;
        private readonly SqlDBHelper _dBHelper;
        private readonly Mapper _mapper;
        //string ss = _configuration.GetConnectionString("defaultConnection").ToString();
        public Demo(IConfiguration configuration)
        {
            _configuration = configuration;
            _dBHelper = new SqlDBHelper(configuration);
            
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
            dt = _dBHelper.GetDatatable("USP_JCRUD_USERS", CommandType.StoredProcedure, sqlParameters);


            //GET DATATABLE USING SELECT QUERY
            //sqlParameters.Add(new SqlParameter("@test", "NODE"));
            //dt = _dBHelper.GetDatatable("SELECT * FROM [USERS] WHERE FirstName = @test",CommandType.Text,sqlParameters);



            //GET DATASET USING SELECT QUERY
            // sqlParameters.Add(new SqlParameter("@test", "NODE"));
            // DataSet dS = _dBHelper.GetDataSet("SELECT * FROM [USERS] WHERE FirstName = @test",CommandType.Text,sqlParameters);
            List<User> result = JsonConvert.DeserializeObject<List<User>>(JsonConvert.SerializeObject(dt)); ;
            return result;
        }

    }
}
