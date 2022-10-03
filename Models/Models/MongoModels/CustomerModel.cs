using DbEntities.Models.MongoModels;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbEntities.Models.MongoModel
{
    public class CustomerModel : BaseModel
    {
        
        
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string gender { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string address { get; set; }
        public DateTime dob { get; set; }
        
       
     
    }
}
