using System;
using System.Collections.Generic;
using System.Text;

namespace CommonEntities.Models.ApiModels
{
    public class ResponseModel<T>
    {
        public int code { get; set; }
        public string message { get; set; }

        public string status { get; set; }
        public T data { get; set; }
    }
}
