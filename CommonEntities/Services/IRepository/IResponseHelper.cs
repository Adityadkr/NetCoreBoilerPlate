using CommonEntities.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonEntities.Services.IRepository
{
    public interface IResponseHelper<T>
    {
        ResponseModel<T> CreateResponse(int code, string message, string status, T data);
    }
}
