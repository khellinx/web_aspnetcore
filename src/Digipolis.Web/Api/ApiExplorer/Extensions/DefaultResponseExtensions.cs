using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.Web.Api.ApiExplorer
{
    public static class DefaultResponseExtensions
    {
        /// <summary>
        /// Defines a filter for DefaultResponseType that the api method should equal GET in order to be added to the api description.
        /// </summary>
        /// <param name="defaultResponseType">The original default response type.</param>
        /// <returns>The original default response type with an added filter.</returns>
        public static DefaultResponseType ToGet(this DefaultResponseType defaultResponseType)
        {
            defaultResponseType.ToMethod("get");

            return defaultResponseType;
        }

        /// <summary>
        /// Defines a filter for DefaultResponseType that the api method should equal POST in order to be added to the api description.
        /// </summary>
        /// <param name="defaultResponseType">The original default response type.</param>
        /// <returns>The original default response type with an added filter.</returns>
        public static DefaultResponseType ToPost(this DefaultResponseType defaultResponseType)
        {
            defaultResponseType.ToMethod("post");

            return defaultResponseType;
        }

        /// <summary>
        /// Defines a filter for DefaultResponseType that the api method should equal PUT in order to be added to the api description.
        /// </summary>
        /// <param name="defaultResponseType">The original default response type.</param>
        /// <returns>The original default response type with an added filter.</returns>
        public static DefaultResponseType ToPut(this DefaultResponseType defaultResponseType)
        {
            defaultResponseType.ToMethod("put");

            return defaultResponseType;
        }

        /// <summary>
        /// Defines a filter for DefaultResponseType that the api method should equal DELETE in order to be added to the api description.
        /// </summary>
        /// <param name="defaultResponseType">The original default response type.</param>
        /// <returns>The original default response type with an added filter.</returns>
        public static DefaultResponseType ToDelete(this DefaultResponseType defaultResponseType)
        {
            defaultResponseType.ToMethod("delete");

            return defaultResponseType;
        }

        /// <summary>
        /// Defines a filter for DefaultResponseType that the api method should equal the provided value in order to be added to the api description.
        /// </summary>
        /// <param name="defaultResponseType">The original default response type</param>
        /// <param name="httpMethod">The HTTP method should equal the provided value.</param>
        /// <returns>The original default response type with an added filter.</returns>
        public static DefaultResponseType ToMethod(this DefaultResponseType defaultResponseType, string httpMethod)
        {
            defaultResponseType.When(apiDesc => apiDesc.HttpMethod.Equals(httpMethod, StringComparison.OrdinalIgnoreCase));

            return defaultResponseType;
        }

        /// <summary>
        /// Defines a filter for DefaultResponseType that a route parameter has to be present in order to be added to the api description.
        /// </summary>
        /// <param name="defaultResponseType">The original default response type.</param>
        /// <param name="paramNameEquals">The name of the route parameter should equal the provided value. When this value is not null, the paramNameContains value will be ignored. Comparison is case insensitive.</param>
        /// <param name="paramNameContains">The name of the route parameter should contain the provided value. This value will be ignored if paramNameEquals is not null. Comparison is case insensitive.</param>
        /// <returns>The original default response type with an added filter.</returns>
        public static DefaultResponseType WhenHasRouteParameter(this DefaultResponseType defaultResponseType, string paramNameEquals = null, string paramNameContains = null)
        {
            if (paramNameContains != null && paramNameEquals != null)
            {
                throw new ArgumentException($"Parameters {nameof(paramNameContains)} and {nameof(paramNameEquals)} cannot be specified both.");
            }

            if (paramNameEquals != null)
            {
                defaultResponseType.When(apiDesc => apiDesc.ParameterDescriptions.Any(x => x.Source.Id == "Path" && x.Name.Equals(paramNameEquals, StringComparison.OrdinalIgnoreCase)));
            }
            else if (paramNameContains != null)
            {
                defaultResponseType.When(apiDesc => apiDesc.ParameterDescriptions.Any(x => x.Source.Id == "Path" && x.Name.Contains(paramNameEquals, StringComparison.OrdinalIgnoreCase)));
            }
            else
            {
                defaultResponseType.When(apiDesc => apiDesc.ParameterDescriptions.Any(x => x.Source.Id == "Path"));
            }

            return defaultResponseType;
        }

        /// <summary>
        /// Defines a filter with a specific delegate function. If the filter passes, the response type will be added to the api description.
        /// </summary>
        /// <param name="defaultResponseType">The original default response type.</param>
        /// <param name="filter">The specific delegate function that has to return true in order to be added to the api description.</param>
        /// <returns>The original default response type with an added filter.</returns>
        public static DefaultResponseType When(this DefaultResponseType defaultResponseType, Func<ApiDescription, bool> filter)
        {
            defaultResponseType.Filters.Add(filter);

            return defaultResponseType;
        }

        /// <summary>
        /// Specifies that the default response type should be added to the api description, even if that description does not have the ProducesDefaultResponseTypes attribute.
        /// </summary>
        /// <param name="defaultResponseType">The original default response type.</param>
        /// <returns>The original default response type with the property AddToDescriptionsWithoutDefaultAttribute set to true.</returns>
        public static DefaultResponseType EvenWithoutDefaultAttribute(this DefaultResponseType defaultResponseType)
        {
            defaultResponseType.AddToDescriptionsWithoutDefaultAttribute = true;

            return defaultResponseType;
        }

        /// <summary>
        /// Specifies that the default response type produces a specific mediatype and optional outputFormatter.
        /// </summary>
        /// <param name="defaultResponseType">The original default response type.</param>
        /// <param name="mediaType">The media type the api description produces for this specific response type.</param>
        /// <param name="outputFormatter">The output formatter to be used for this specific response type.</param>
        /// <returns>The original default response type with an added mediatype and outputformatter combination.</returns>
        public static DefaultResponseType Produces(this DefaultResponseType defaultResponseType, string mediaType, IOutputFormatter outputFormatter = null)
        {
            defaultResponseType.ResponseType.ApiResponseFormats.Add(new ApiResponseFormat()
            {
                MediaType = mediaType,
                Formatter = outputFormatter
            });

            return defaultResponseType;
        }
    }
}
