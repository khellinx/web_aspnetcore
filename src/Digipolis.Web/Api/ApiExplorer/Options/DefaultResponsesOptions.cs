using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.Web.Api.ApiExplorer
{
    public class DefaultResponsesOptions : DescriptionProviderOptions<DefaultResponsesApiDescriptionProvider>
    {
        private ICollection<DefaultResponseType> _defaultResponseTypes;
        public ICollection<DefaultResponseType> DefaultResponseTypes
        {
            get
            {
                if (_defaultResponseTypes == null) _defaultResponseTypes = new List<DefaultResponseType>();

                return _defaultResponseTypes;
            }
            private set
            {
                _defaultResponseTypes = value;
            }
        }

        private ICollection<ApiResponseFormat> _defaultResponseFormats;
        public ICollection<ApiResponseFormat> DefaultResponseFormats
        {
            get
            {
                if (_defaultResponseFormats == null) _defaultResponseFormats = new List<ApiResponseFormat>();

                return _defaultResponseFormats;
            }
            private set
            {
                _defaultResponseFormats = value;
            }
        }

        public DefaultResponseType AddDefaultResponseType(int statusCode, Type responseType = null)
        {
            return AddDefaultResponseType(new ApiResponseType() { StatusCode = statusCode, Type = responseType });
        }

        public DefaultResponseType AddDefaultResponseType(ApiResponseType responseType)
        {
            var defaultResponseType = new DefaultResponseType()
            {
                ResponseType = responseType
            };

            DefaultResponseTypes.Add(defaultResponseType);

            return defaultResponseType;
        }

        public void AddDefaultResponseFormat(string mediaType, IOutputFormatter outputFormatter = null)
        {
            AddDefaultResponseFormat(new ApiResponseFormat() { MediaType = mediaType, Formatter = outputFormatter });
        }

        public void AddDefaultResponseFormat(ApiResponseFormat responseFormat)
        {
            DefaultResponseFormats.Add(responseFormat);
        }
    }
}
