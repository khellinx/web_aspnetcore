using Digipolis.Errors;
using Digipolis.Web.Api.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Digipolis.Web.Startup
{
    public static class ApiExplorerServiceExtensions
    {
        public static IServiceCollection AddApiDescriptionProvider<TApiDescriptionProvider>(this IServiceCollection services, int order = 0)
            where TApiDescriptionProvider : class, IApiDescriptionProvider
        {
            services.Configure<DescriptionProviderOptions<TApiDescriptionProvider>>(options => options.Order = order);
            services.TryAddEnumerable(ServiceDescriptor.Transient<IApiDescriptionProvider, TApiDescriptionProvider>());

            return services;
        }

        public static IServiceCollection AddDefaultResponsesApiDescriptionProvider(this IServiceCollection services, Action<DefaultResponsesOptions> setupAction = null)
        {
            if (setupAction == null)
            {
                setupAction = options =>
                {
                    options.Order = 0;

                    // All responses produce json
                    options.AddDefaultResponseFormat("application/json");

                    // 404 - Not Found
                    options.AddDefaultResponseType((int)HttpStatusCode.NotFound, typeof(Error)).ToGet().WhenHasRouteParameter(paramNameContains: "id");
                    options.AddDefaultResponseType((int)HttpStatusCode.NotFound, typeof(Error)).ToPut().WhenHasRouteParameter(paramNameContains: "id");
                    options.AddDefaultResponseType((int)HttpStatusCode.NotFound, typeof(Error)).ToDelete().WhenHasRouteParameter(paramNameContains: "id");

                    // 400 - Bad Request
                    options.AddDefaultResponseType((int)HttpStatusCode.BadRequest, typeof(Error)).ToPost();
                    options.AddDefaultResponseType((int)HttpStatusCode.BadRequest, typeof(Error)).ToPut();

                    // 500 - Internal server Error
                    options.AddDefaultResponseType((int)HttpStatusCode.InternalServerError, typeof(Error));
                };
            }

            services.Configure<DefaultResponsesOptions>(setupAction);
            services.TryAddEnumerable(ServiceDescriptor.Transient<IApiDescriptionProvider, DefaultResponsesApiDescriptionProvider>());

            return services;
        }
    }
}
