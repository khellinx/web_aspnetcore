using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.Web.Api.ApiExplorer
{
    public class DefaultResponseType
    {
        private ICollection<Func<ApiDescription, bool>> _filters;

        /// <summary>
        /// The filters for the ApiDescription to match
        /// </summary>
        /// <remarks>
        /// The ApiDescription must pass each filter in order for the ResponseType to be added
        /// </remarks>
        public ICollection<Func<ApiDescription, bool>> Filters
        {
            get
            {
                if (_filters == null) _filters = new List<Func<ApiDescription, bool>>();

                return _filters;
            }
        }

        /// <summary>
        /// The response type to be added to the ApiDescription
        /// </summary>
        public ApiResponseType ResponseType { get; set; }

        public bool AddToDescriptionsWithoutDefaultAttribute { get; set; }
    }
}
