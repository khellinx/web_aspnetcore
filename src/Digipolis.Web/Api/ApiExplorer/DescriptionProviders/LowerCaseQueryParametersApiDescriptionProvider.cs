using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.Web.Api.ApiExplorer
{
    public class LowerCaseQueryParametersApiDescriptionProvider : IApiDescriptionProvider
    {
        public LowerCaseQueryParametersApiDescriptionProvider(IOptions<DescriptionProviderOptions<LowerCaseQueryParametersApiDescriptionProvider>> options)
        {
            Order = options?.Value?.Order ?? 0;
        }

        public int Order { get; private set; }

        public void OnProvidersExecuted(ApiDescriptionProviderContext context)
        {
        }

        public void OnProvidersExecuting(ApiDescriptionProviderContext context)
        {
            foreach (var parameter in context.Results.SelectMany(x => x.ParameterDescriptions).Where(x => x.Source.Id == "Query"))
            {
                parameter.Name = parameter.Name.ToLower();
            }
        }
    }
}
