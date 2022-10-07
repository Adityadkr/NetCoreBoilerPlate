using DbEntities.Models.MongoModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbServices.IRepositories
{
    public interface IEmployee
    {
        UserModel AddEmployee(EmployeeModel employee);
    }
}
