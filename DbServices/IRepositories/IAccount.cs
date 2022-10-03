using DbEntities.Models.MongoModels;
using DbEntities.Models.MongoModels.RequestModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbServices.IRepositories
{
    public interface IAccount
    {
        UserModel Register(RegistrationModels registration);
        UserModel GetUser(LoginModel login);
    }
}
