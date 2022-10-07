using DbEntities.Models.MongoModels;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbServices.IRepositories
{
    public interface IBranch
    {
        BranchModel Addbranch(BranchModel branch);
        List<BranchModel> GetBranches();
    }
}
