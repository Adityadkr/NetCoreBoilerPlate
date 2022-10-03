using AutoMapper;
using DbEntities.Models.MongoModel;
using DbEntities.Models.MongoModels;
using DbEntities.Models.MongoModels.RequestModels;
using DbServices.Helpers;
using DbServices.IRepositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbServices.Repositories
{
    public class Account: IAccount
    {

        private readonly IConfiguration _configuration;

        private readonly MongoDBHelper _mongodBHelper;
        private readonly Mapper _mapper;
        public Account(IConfiguration configuration)
        {
            _configuration = configuration;
            _mongodBHelper = new MongoDBHelper("TripPlanner", _configuration);
        }
        public UserModel Register(RegistrationModels registration)
        {
            var customer = new CustomerModel();
            customer._id = ObjectId.GenerateNewId();
            customer.firstname = registration.firstname;
            customer.lastname = registration.lastname;
            var data = _mongodBHelper.InsertOne<CustomerModel>("customers", customer);
            var user = new UserModel();
            user.insertedId = customer._id.ToString();
            user.username = registration.username;
            user.password = registration.password;
            user.email = registration.email;
            user.role = "CUSTOMER";
            _mongodBHelper.InsertOne<UserModel>("users", user);
            return user;
        }
        public UserModel GetUser(LoginModel login)
        {
           
            var userExists = _mongodBHelper.GetByKey<UserModel>("users", x => x.username == login.username && x.password == login.password);
            if (userExists != null)
            {
                return userExists;
            }
            else
            {
                return new UserModel();
            }


        }
    }
}
