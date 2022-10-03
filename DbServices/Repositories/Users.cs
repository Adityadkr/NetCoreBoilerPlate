using AutoMapper;
using DbEntities.Models.MongoModels;
using DbEntities.Models.MongoModels.RequestModels;
using DbServices.Helpers;
using DbServices.IRepositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbServices.Repositories
{
    public class Users : IUsers
    {
        private readonly IConfiguration _configuration;

        private readonly MongoDBHelper _mongodBHelper;
        private readonly Mapper _mapper;

        public Users(IConfiguration configuration)
        {
            _configuration = configuration;
            _mongodBHelper = new MongoDBHelper("TripPlanner", _configuration);
        }
        public string UserExists(LoginModel user)
        {
            var userExists = _mongodBHelper.GetByKey<UserModel>("users", x => x.username == user.username);
            if (userExists != null)
            {
                if (userExists.password == user.password)
                {
                    if (userExists.status == "A")
                    {
                        return "exists";

                    }
                    return "Sorry your account has been deactivated";
                }
                return "Sorry, You have entered wrong password";
            }
            else
            {
                return "User Not Found!!";
            }


        }
        
        public UserModel AddUser(UserModel user)
        {
            var data = _mongodBHelper.InsertOne<UserModel>("users", user);
            var userExists = _mongodBHelper.GetByKey<UserModel>("users", x => x.username == user.username);
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
