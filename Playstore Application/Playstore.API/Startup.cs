using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Playstore.Infrastructure;
using Playstore.Core;
using Microsoft.AspNetCore.Mvc;
using Playstore.Core.Security;
using Serilog;
using Playstore.JsonSerialize;
using System;
using Playstore.Contracts.DTO;
using Playstore.Contracts.Middleware;
using Serilog.Sinks.MSSqlServer;

namespace Playstore
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
            services.AddTransient<JwtAuthenticationMiddleware>();
            services.AddSession(options => // Session Configuration
            {
                options.IdleTimeout = TimeSpan.FromSeconds(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddPersistence(Configuration);
            services.AddCore();
            services.AddDistributedMemoryCache(); // Distributed cache for session  
            services.AddHttpContextAccessor();
            services.AddMarketplaceAuthentication(Configuration);
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.Configure<EmailConfig>(Configuration.GetSection("EmailConfig"));

            services.AddControllers().AddJsonOptions(option =>
            {
                option.JsonSerializerOptions.Converters.Add(new JsonConvertor());
            })
            .AddNewtonsoftJson(option =>
            {
                option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File($"Logs/{DateOnly.FromDateTime(DateTime.Today)}.txt", rollingInterval: RollingInterval.Day)
            .WriteTo.Console()
            .WriteTo.MSSqlServer(
                connectionString: "data source = ASPLAP1695\\SQLEXPRESS; initial catalog= Playstore App; integrated security= SSPI; Trusted_Connection= true;Encrypt= true; Trust Server Certificate = true",
                sinkOptions: new MSSqlServerSinkOptions
                {
                    TableName = "Logs",
                    AutoCreateSqlTable = true,

                })
            .CreateLogger();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(Configuration.GetValue<string>("Authentication:Jwt:ValidAudience"));
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowCredentials();
                });
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Aspire Marketplace App API", Description = "Aspire Marketplace App  is a  solution, built to demonstrate implementing market place to sell/download the app which present in the market place ", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<JwtAuthenticationMiddleware>();
            app.UseHttpsRedirection();


            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            app.UseSession();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aspire Playstore App API v1");
            });

            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}