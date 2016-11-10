using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.Web.Api.ApiExplorer
{
    public class ConsumesJsonApiDescriptionProvider : IApiDescriptionProvider
    {
        public ConsumesJsonApiDescriptionProvider(IOptions<DescriptionProviderOptions<ConsumesJsonApiDescriptionProvider>> options)
        {
            Order = options?.Value?.Order ?? 0;
        }

        public int Order { get; private set; }

        public void OnProvidersExecuted(ApiDescriptionProviderContext context)
        {
        }

        public void OnProvidersExecuting(ApiDescriptionProviderContext context)
        {
            foreach (var apiDescription in context.Results)
            {
                apiDescription.SupportedRequestFormats.Add(new ApiRequestFormat()
                {
                    Formatter = null,
                    MediaType = "application/json"
                });
            }
        }
    }
}
