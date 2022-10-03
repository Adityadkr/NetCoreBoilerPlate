using AutoMapper;
using DbEntities.Models.MongoModel;
using DbEntities.Models.MongoModels;
using DbServices.Helpers;
using DbServices.IRepositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbServices.Repositories
{
    public class Customers : ICustomer
    {
        private readonly IConfiguration _configuration;

        private readonly MongoDBHelper _mongodBHelper;
        private readonly Mapper _mapper;
        public Customers(IConfiguration configuration)
        {
            _configuration = configuration;
            _mongodBHelper = new MongoDBHelper("TripPlanner", _configuration);
        }
        public List<CustomerModel> GetCustomers()
        {
            return _mongodBHelper.Get<CustomerModel>("customers");




        }
        public CustomerModel AddCustomer(CustomerModel customer)
        {
            var data = _mongodBHelper.InsertOne<CustomerModel>("customers", customer);
            return new CustomerModel();

        }
        public CustomerModel GetCustomer(string id)
        {
            return _mongodBHelper.GetByKey<CustomerModel>("customers", x => x._id == ObjectId.Parse(id));
        }
    }
}
