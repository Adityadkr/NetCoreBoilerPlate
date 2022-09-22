﻿using CommonEntities.Helpers;
using CommonEntities.Objects;
using DbEntities;
using DbEntities.Models.MongoModels;
using DbServices.IRepositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApp.Filters;
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

       // [TypeFilter(typeof(GlobalExceptionFilter))]
        public async Task<IActionResult> Index()
        {
            //var data = _demo.getUsers();n
            try
            {
                User user = new User();
                user.FirstName = "NODE";
                var response = await HttpHelper.Post("https://localhost:44309/api/Account/login", user, ContentType.JSON);

                var content = await response.Content.ReadAsStringAsync();
                var claims = new[] {

                new Claim("FirstName","test")
                };
                SessionHelper.SignInAsync(claims, HttpContext);

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            // var string1 = this.User.FindFirst("FirstName").Value;
            var data = _demo.GetAliens();

            var findById = _demo.GetAliensByID("6311b676bc2c8b0ae3c0c3e6");

            Alien alien = new Alien();
            alien._id = ObjectId.Parse("632b5d0a5677012af4bc986d");
            alien.name = "mongodb123";
            alien.pass = "123";
            alien.tech = "nosql23";
            alien.sub = false;
            alien.__v = 0;

            //bool resultAdd = await _demo.AddAlien(alien);
            //bool resultAdd = await _demo.UpdateAlien(alien);
            bool resultDelete =   _demo.DeleteAlien(alien); 
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
