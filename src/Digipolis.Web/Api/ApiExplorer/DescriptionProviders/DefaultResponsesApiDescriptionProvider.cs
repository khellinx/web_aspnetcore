using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.Web.Api.ApiExplorer
{
    public class DefaultResponsesApiDescriptionProvider : IApiDescriptionProvider
    {
        private DefaultResponsesOptions _options;

        public DefaultResponsesApiDescriptionProvider(IOptions<DefaultResponsesOptions> options)
        {
            _options = options?.Value ?? new DefaultResponsesOptions();
            Order = options?.Value?.Order ?? 0;
        }

        public int Order { get; private set; }

        public void OnProvidersExecuted(ApiDescriptionProviderContext context)
        {
        }

        public void OnProvidersExecuting(ApiDescriptionProviderContext context)
        {
            foreach (var actionDescription in context.Results)
            {
                var defaultResultTypesAttribute = actionDescription.ActionDescriptor.GetCustomControllerAttribute<ProducesDefaultResponsesAttribute>();

                // Add the success code
                if (defaultResultTypesAttribute != null)
                {
                    actionDescription.AddSupportedResponseTypeIfNotExists(defaultResultTypesAttribute.SuccessStatusCode, defaultResultTypesAttribute.SuccessResponseType);
                }

                // Add default codes
                foreach (var responseType in _options.DefaultResponseTypes)
                {
                    if (ApiDescriptionFulfillsAllFilters(actionDescription, responseType.Filters) && (defaultResultTypesAttribute != null || responseType.AddToDescriptionsWithoutDefaultAttribute))
                    {
                        actionDescription.AddSupportedResponseTypeIfNotExists(responseType.ResponseType);
                    }
                }

                // Add default response formats
                if (defaultResultTypesAttribute != null)
                {
                    foreach (var responseFormat in actionDescription.SupportedResponseTypes)
                    {
                        foreach (var defaultResponseFormat in _options.DefaultResponseFormats)
                        {
                            if (!responseFormat.ApiResponseFormats.Any(x => x.MediaType.Equals(defaultResponseFormat.MediaType, StringComparison.OrdinalIgnoreCase)))
                            {
                                responseFormat.ApiResponseFormats.Add(defaultResponseFormat);
                            }
                        }
                    }
                }
            }
        }

        private bool ApiDescriptionFulfillsAllFilters(ApiDescription apiDescription, IEnumerable<Func<ApiDescription, bool>> filters)
        {
            foreach (var filter in filters)
            {
                if (!filter(apiDescription)) return false;
            }

            return true;
        }
    }
}
