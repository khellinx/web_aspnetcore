﻿using AutoMapper;
using Digipolis.Errors;
using Digipolis.Web;
using Digipolis.Web.Api.ApiExplorer;
using Digipolis.Web.SampleApi.Configuration;
using Digipolis.Web.SampleApi.Data;
using Digipolis.Web.SampleApi.Logic;
using Digipolis.Web.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Swashbuckle.Swagger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Digipolis.Web.SampleApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc().AddVersionEndpoint().AddApiExtensions(Configuration.GetSection("ApiExtensions"), x =>
            {
                //Override settings made by the appsettings.json
                x.PageSize = 10;
            });

            services.AddGlobalErrorHandling<ApiExceptionMapper>();

            // Add default response types
            services.AddDefaultResponsesApiDescriptionProvider();

            // Add other description providers
            services.AddApiDescriptionProvider<LowerCaseRelativePathApiDescriptionProvider>(10);
            services.AddApiDescriptionProvider<LowerCaseQueryParametersApiDescriptionProvider>(11);
            services.AddApiDescriptionProvider<ConsumesJsonApiDescriptionProvider>(30);

            // Add Swagger extensions
            services.AddSwaggerGen<ApiExtensionSwaggerSettings>(x =>
            {
                //Specify Api Versions
                x.MultipleApiVersions(new[] { new Info
                {
                    //Add Inline version
                    Version = Versions.V1,
                    Title = "API V1",
                    Description = "Description for V1 of the API",
                    Contact = new Contact { Email = "info@digipolis.be", Name = "Digipolis", Url = "https://www.digipolis.be" },
                    TermsOfService = "https://www.digipolis.be/tos",
                    License = new License
                    {
                        Name = "My License",
                        Url = "https://www.digipolis.be/licensing"
                    },
                },
                //Add version through configuration class
                new Version2()});
            });

            //Register Dependencies for example project
            services.AddScoped<IValueRepository, ValueRepository>();
            services.AddScoped<IValueLogic, ValueLogic>();

            //Add AutoMapper
            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // Enable Api Extensions
            app.UseApiExtensions();

            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUi();
        }
    }
}
