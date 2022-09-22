using DbEntities;
using DbEntities.Models.MongoModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DbServices.IRepositories
{
    public interface IDemo
    {

        List<User> getUsers();
        List<Alien> GetAliens();
        Alien GetAliensByID(string id);
        //Task<bool> AddAlien(Alien alien);
        //Task<bool> UpdateAlien(Alien alien);
        //Task<bool> DeleteAlien(Alien alien);
        bool AddAlien(Alien alien);
        bool UpdateAlien(Alien alien);
        bool DeleteAlien(Alien alien);
    }
}
