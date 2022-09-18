using DbEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DbServices.IRepositories
{
    public interface IDemo
    {
        List<User> getUsers();
    }
}
