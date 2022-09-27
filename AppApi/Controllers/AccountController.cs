using CommonEntities.Helpers;
using CommonEntities.Services.IRepository;
using DbEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IJwtService _jwtService;
       
        public AccountController(IConfiguration config, IJwtService jwtService)
        {
            _config = config;
            _jwtService = jwtService;
        }
        [HttpPost]
        [Route("login")]      
        public IActionResult Login(User user) 
        { 
            if (user.FirstName == "NODE") 
            {
                var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("FirstName", user.FirstName)
                };
                string token = _jwtService.GenerateJSONWebToken(claims);
                return Ok(token);
            }
            return Ok();
        }
    }
}
