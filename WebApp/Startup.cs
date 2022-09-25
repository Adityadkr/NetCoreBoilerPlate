using CommonEntities.Models.Email;
using CommonEntities.Services.IRepository;
using CommonEntities.Services.Repository;
using DbServices.IRepositories;
using DbServices.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Filters;

namespace WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            // services.AddControllersWithViews(config => config.Filters.Add(typeof(GlobalExceptionFilter)));

            services.AddMvc();

            #region Authentication
            services.AddAuthentication(x =>
             {
                 x.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                 x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

             }).AddCookie();
            #endregion


            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/AccessDenied");
            //});

            #region Repositories
            services.AddScoped<IDemo, Demo>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IJwtService, JwtService>();
            #endregion


            #region Caching
            services.AddMemoryCache();
            #endregion

            #region Email
            var emailConfig = Configuration
            .GetSection("EmailConfiguration")
            .Get<EmailConfigurationModel>();
            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailService, EmailService>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();



            }
            else
            {
                app.UseMiddleware(typeof(GlobalExceptionFilter));
             //  app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePages(async context =>
                {
                    if (context.HttpContext.Response.StatusCode == 400)
                    {
                        context.HttpContext.Response.Redirect("Errors?error=400");
                    }
                    else if (context.HttpContext.Response.StatusCode == 404)
                    {
                        context.HttpContext.Response.Redirect("Errors?error=404");
                    }
                });

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            #region logging
            var Logs = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
            var YearPath = Path.Combine(Logs, DateTime.Now.Year.ToString());
            var MonthPath = Path.Combine(YearPath, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month).ToString());
            if (!Directory.Exists(MonthPath))
            {
                Directory.CreateDirectory(MonthPath);
            }
            var dateFile = DateTime.Now.ToString("dd_MM_yyyy") + ".txt";
            var path = Path.Combine(MonthPath, dateFile);
            loggerFactory.AddFile(path);
            #endregion
        }
    }
}
