using CommonEntities.Models.ApiModels;
using CommonEntities.Services.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AppApi.Filters
{
    public class GlobalExceptionHandler
    {
        public RequestDelegate requestDelegate;
        public ILogger<GlobalExceptionHandler> logger;
       
        public GlobalExceptionHandler(RequestDelegate requestDelegate, ILogger<GlobalExceptionHandler> loggers)
        {
            this.requestDelegate = requestDelegate;
            this.logger = loggers;
      
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                //await HandleException(context);
                await requestDelegate(context);
            }
            catch (Exception ex)
            {
                logger.LogError("------------------------Exception------------------------------");
                logger.LogError($"Error:{ex.Message},\n StackTrace{ex.StackTrace}");
                await HandleException(context, ex);
            }
        }
        private static Task HandleException(HttpContext context, Exception ex)
        {
            var errorMessage = JsonConvert.SerializeObject(new ErroModel  { message = ex.Message, code = (int)HttpStatusCode.BadRequest,status = "failure"});
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            
            return context.Response.WriteAsync(errorMessage);
        }
    }
}
