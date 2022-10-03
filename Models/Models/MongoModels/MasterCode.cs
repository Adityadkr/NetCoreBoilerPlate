using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbEntities.Models.MongoModels
{
    public class MasterCode:BaseModel
    {
        public string key1 { get; set; }
        public string key2 { get; set; }
        public string pcode { get; set; }
        public string code { get; set; }
        public string codedescription { get; set; }
        
    }
}
