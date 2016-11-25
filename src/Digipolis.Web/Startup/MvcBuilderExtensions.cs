﻿using System;
using Digipolis.Errors;
using Digipolis.Web.Api;
using Digipolis.Web.Api.Conventions;
using Digipolis.Web.Api.Filters;
using Digipolis.Web.Api.JsonConverters;
using Digipolis.Web.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Digipolis.Web.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.DotNet.InternalAbstractions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Digipolis.Web
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddApiExtensions(this IMvcBuilder builder, IConfigurationSection config = null, Action<ApiExtensionOptions> build = null, Type exception = null)
        {
            var apiOptions = new ApiExtensionOptions();

            #region Include services needed for building uri's in the paging object

            builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            #endregion

            #region Register Options

            if (config != null && build != null) builder.Services.Configure<ApiExtensionOptions>(x => { });
            if (config != null)
            {
                builder.Services.Configure<ApiExtensionOptions>(config);
                config.Bind(apiOptions);
            }
            if (build != null)
            {
                builder.Services.Configure<ApiExtensionOptions>(build);
                build(apiOptions);
            }

            #endregion

            #region Configuration from options

            if (!apiOptions.DisableGlobalErrorHandling)
            {
                builder.AddMvcOptions(options =>
                {
                    options.Filters.Add(typeof(GlobalExceptionFilter));
                });
            }

            if (!apiOptions.DisableVersioning)
            {
                builder.AddMvcOptions(options =>
                {
                    options.Conventions.Insert(0, new RouteConvention(new RouteAttribute("{apiVersion}")));
                });
            }

            #endregion

            builder.AddMvcOptions(options =>
            {
                options.Filters.Insert(0, new PagedResultAttribute());
            });

            builder.AddJsonOptions(x =>
            {
                x.SerializerSettings.ContractResolver = new BaseContractResolver();
                x.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                x.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                x.SerializerSettings.Converters.Add(new TimeSpanConverter());
                x.SerializerSettings.Converters.Add(new GuidConverter());
                x.SerializerSettings.Formatting = Formatting.None;
            });

            return builder;
        }

        public static IMvcBuilder AddVersionEndpoint(this IMvcBuilder builder, Action<WebVersioningOptions> setupAction = null)
        {
            if (setupAction != null)
            {
                builder.Services.Configure(setupAction);
            }

            builder.Services.TryAddSingleton<IVersionProvider, WebVersionProvider>();
            builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<MvcOptions>, WebVersioningOptionsSetup>());

            return builder;
        }
    }
}
