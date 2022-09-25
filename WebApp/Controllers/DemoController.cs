using DbEntities.Models.MongoModels;
using DbServices.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class DemoController : Controller
    {
        private readonly IDemo _demo;
        public DemoController(IDemo demo)
        {
            _demo = demo;
        }
        public IActionResult Index()
        {

            //throw new Exception("system");
            return View();
        }
        public List<Alien> GetAliens() {

            var data = _demo.GetAliens();
            return data;
        }
    }
}
