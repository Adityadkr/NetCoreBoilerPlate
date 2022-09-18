using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApp.Helpers
{
    public static class SessionHelper
    {

        public class SignInObj
        {
            public AuthenticationProperties authenticationProperties { get; set; }
            public ClaimsPrincipal claimsPrincipal { get; set; }
            
        }
        public static void SignInAsync(Claim[] claim,HttpContext httpContext) 
        {
            
            var identity = new ClaimsIdentity();
            identity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var authProperties = new AuthenticationProperties
            {

                IsPersistent = false,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(120)


            };
            httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal, authProperties);
           
        }
        public static ClaimsPrincipal GetCurrentUser(ClaimsPrincipal p) {
            return new ClaimsPrincipal(p);
        }
    }
}
