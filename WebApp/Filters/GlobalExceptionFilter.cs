using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Filters
{
    public class GlobalExceptionFilter
    {
        private ILogger<GlobalExceptionFilter> logger;


        public RequestDelegate requestDelegate;

        public GlobalExceptionFilter(RequestDelegate requestDelegate, ILogger<GlobalExceptionFilter> loggers)
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
                context.Response.Redirect("/Home/Error");
            }
        }
        //public void OnException(ExceptionContext context)
        //{
        //    logger.LogError("------------------------Exception------------------------------");
        //    logger.LogError($"Error:{context.Exception.Message},\n StackTrace{context.Exception.StackTrace}");
        //    // var result = new ViewResult { ViewName = "Error" };
        //    // result.ViewData.Add("Exception", context.Exception);

        //    // Here we can pass additional detailed data via ViewData
        //    //context.ExceptionHandled = true; // mark exception as handled
        //    //context.Result = result;

        //}
    }
}
