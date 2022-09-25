using AppApi.Filters;
using CommonEntities.Models.Email;
using CommonEntities.Services.IRepository;
using CommonEntities.Services.Repository;
using DbServices.IRepositories;
using DbServices.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppApi
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
            services.AddControllers();
            services.AddSwaggerGen(option =>
            {
                //option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                {
                new OpenApiSecurityScheme
                {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
                },
                new string[]{}
                }
                });
            });

            #region Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });
            #endregion


            #region Caching
            services.AddMemoryCache();
            #endregion

            #region Repositories
            services.AddScoped<IDemo, Demo>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IResponseHelper, ResponseHelper>();
            services.AddScoped<IJwtService, JwtService>();
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
            app.UseMiddleware(typeof(GlobalExceptionHandler));
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();

            // This middleware serves the Swagger documentation UI
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });

            //UNCOMMENT WHEN YOU WANT TO  USER JWT AUTHENTICATION IN API
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            //app.UseMvc();
            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
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
