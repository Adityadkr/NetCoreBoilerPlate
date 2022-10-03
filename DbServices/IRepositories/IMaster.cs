using DbEntities.Models.MongoModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbServices.IRepositories
{
    public interface IMaster
    {
        MasterCode AddMaster(MasterCode master);
        List<MasterCode> GetMaster();
      // List<MasterCode> GetMaster(string key1, string key2 = null, string pcode = null);
    }
}
