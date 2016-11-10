using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.Web.Api.ApiExplorer
{
    public static class ApiDescriptionExtensions
    {
        public static void AddSupportedResponseTypeIfNotExists(this ApiDescription actionDescription, int statusCode, Type type = null)
        {
            actionDescription.AddSupportedResponseTypeIfNotExists(new ApiResponseType() { Type = type, StatusCode = statusCode });
        }

        public static void AddSupportedResponseTypeIfNotExists(this ApiDescription actionDescription, ApiResponseType responseType)
        {
            if (responseType == null) throw new ArgumentNullException(nameof(responseType));

            if (actionDescription.SupportedResponseTypes.Any(x => x.StatusCode == responseType.StatusCode)) return;

            actionDescription.SupportedResponseTypes.Add(responseType);
        }
    }
}
