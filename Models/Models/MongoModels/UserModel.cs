using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbEntities.Models.MongoModels
{
   public  class UserModel:BaseModel
    {

        public ObjectId _detailId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string token { get; set; }
        public bool is_first_time { get; set; } = true;
           
    }
}
