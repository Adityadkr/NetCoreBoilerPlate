using DbEntities.Models.MongoModels;
using DbServices.Helpers;
using DbServices.IRepositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbServices.Repositories
{
    public class Employee:IEmployee
    {
        private readonly IConfiguration _configuration;

        private readonly MongoDBHelper _mongodBHelper;


        public Employee(IConfiguration configuration)
        {
            _configuration = configuration;
            _mongodBHelper = new MongoDBHelper("TripPlanner", _configuration);
        }

        public UserModel AddEmployee(EmployeeModel employee)
        {
            try
            {
                _mongodBHelper.InsertOne<EmployeeModel>("employee", employee);
                var data = _mongodBHelper.Get<EmployeeModel>("employee");
                if (data[data.Count - 1].username == employee.username)
                {
                    var user = new UserModel
                    {
                        username = employee.username,
                        email = employee.email,
                        mobile = employee.mobile,
                        role = employee.role,
                        password = employee.password,
                        is_first_time = true,
                        _detailId = data[data.Count - 1]._id,
                        createdon = DateTime.Now

                    };
                    _mongodBHelper.InsertOne<UserModel>("users", user);
                    var lstuser = _mongodBHelper.Get<UserModel>("users");

                    user._id = lstuser[lstuser.Count - 1]._id;
                    return user;

                }
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
