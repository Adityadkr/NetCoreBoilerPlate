using CommonEntities.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonEntities.Services.IRepository
{
    public interface IResponseHelper
    {
        public ResponseModel<T> CreateResponse<T>(int code, string message, string status, T data);
        public ResponseModel<T> HandleException<T>(Exception ex);
    }
}
