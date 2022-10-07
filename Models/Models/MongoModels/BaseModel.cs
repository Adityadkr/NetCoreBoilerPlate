using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbEntities.Models.MongoModels
{
    public class BaseModel
    {

        public ObjectId _id { get; set; }
        //public string id { get; set; }
         
        public string insertedId { get; set; }
        public string createdby { get; set; }
        public string updatedby { get; set; }
        public DateTime createdon { get; set; }
        public DateTime updatedon { get; set; }
        public string status { get; set; }

      
    }
}
