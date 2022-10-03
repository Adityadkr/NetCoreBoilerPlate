using System;
using System.Collections.Generic;
using System.Text;

namespace DbEntities.Models.MongoModels.ResponseModels
{
    public class LoginResponseModel
    {

        public string username { get; set; }
        public string _id { get; set; }
        public string token { get; set; }
    }
}
