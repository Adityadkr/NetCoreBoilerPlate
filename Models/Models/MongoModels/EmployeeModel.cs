using System;
using System.Collections.Generic;
using System.Text;

namespace DbEntities.Models.MongoModels
{
    public class EmployeeModel:UserModel
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string gender { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string address { get; set; }
        public string designation { get; set; }
        public string reports_to { get; set; }
        public DateTime dob { get; set; }
    }
}
