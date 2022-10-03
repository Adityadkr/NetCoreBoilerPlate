using System;
using System.Collections.Generic;
using System.Text;

namespace DbEntities.Models.MongoModels.RequestModels
{
    public class RegistrationModels
    {
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
    }
}
