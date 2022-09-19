using CommonEntities.Helpers;
using CommonEntities.Objects;
using DbEntities;
using DbServices.IRepositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApp.Helpers;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IDemo _demo;
        public HomeController(IDemo demo, ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _demo = demo;
        }

        public async Task<IActionResult>Index()
        {
            //var data = _demo.getUsers();n
            try
            {
                User user = new User();
                user.FirstName = "NODE";
                var response = await HttpHelper.Post<User>("https://localhost:44309/api/Account/login", user, ContentType.JSON);

                var content = await response.Content.ReadAsStringAsync();
                var claims = new[] {

                new Claim("FirstName","test")
                };
                SessionHelper.SignInAsync(claims, HttpContext);
               
            }
            catch (Exception ex)
            {

            }


            return View();
        }

        public IActionResult Privacy()
        {
           
            var string1 =  this.User.FindFirst("FirstName").Value;

            //var response = HttpHelper.

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
