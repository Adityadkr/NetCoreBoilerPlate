using DbEntities.Models.MongoModels;
using DbEntities.Models.MongoModels.RequestModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbServices.IRepositories
{
    public interface IUsers
    {
        UserModel AddUser(UserModel user);
        string UserExists(LoginModel user);
        List<UserModel> GetUsers(string role = null);
    }
}
