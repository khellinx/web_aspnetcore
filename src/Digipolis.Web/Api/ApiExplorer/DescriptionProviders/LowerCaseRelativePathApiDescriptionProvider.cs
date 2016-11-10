using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digipolis.Web.Api.ApiExplorer
{
    public class LowerCaseRelativePathApiDescriptionProvider : IApiDescriptionProvider
    {
        public LowerCaseRelativePathApiDescriptionProvider(IOptions<DescriptionProviderOptions<LowerCaseRelativePathApiDescriptionProvider>> options)
        {
            Order = options?.Value?.Order ?? 0;
        }

        public int Order { get; private set; }

        public void OnProvidersExecuted(ApiDescriptionProviderContext context)
        {
        }

        public void OnProvidersExecuting(ApiDescriptionProviderContext context)
        {
            foreach (var description in context.Results)
            {
                var relativePathSplit = description.RelativePath.Split('/');

                var newRelativePathBuilder = new StringBuilder();
                for (var i = 0; i < relativePathSplit.Length; i++)
                {
                    var portion = relativePathSplit[i];

                    if (!portion.StartsWith("{") || !portion.EndsWith("}"))
                    {
                        newRelativePathBuilder.Append(portion.ToLowerInvariant());
                    }
                    else
                    {
                        newRelativePathBuilder.Append(portion);
                    }

                    if (i < relativePathSplit.Length - 1)
                    {
                        newRelativePathBuilder.Append("/");
                    }
                }

                description.RelativePath = newRelativePathBuilder.ToString();
            }
        }
    }
}
