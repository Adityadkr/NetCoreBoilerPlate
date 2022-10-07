using System;
using System.Collections.Generic;
using System.Text;

namespace DbEntities.Models.MongoModels
{
    public class BranchModel : BaseModel
    {
        public string branchname { get; set; }
        public string branchaddress { get; set; }
        public string branchcity { get; set; }
        public string branchstate { get; set; }
        public string branchcountry { get; set; }
        public string admin_id { get; set; }
        public bool is_admin_allocated { get; set; } = false;
    }
}
