using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Digipolis.Web.Api
{
    public abstract class PagedResult
    {
        [JsonIgnore]
        public PageOptions PageOptions { get; set; }

        [JsonProperty(PropertyName = "_links")]
        public PagedResultLinks Links { get; set; }

        [JsonProperty(PropertyName = "_page")]
        public Page Page { get; set; }
    }

    public class PagedResult<T> : PagedResult
    {
        [JsonProperty(PropertyName = "_embedded")]
        public PagedResultEmbedded<T> Embedded { get; set; }

        public PagedResult(PageOptions pageOptions, int totalElements, IEnumerable<T> data)
        {
            if (pageOptions == null)
            {
                throw new ArgumentNullException(nameof(pageOptions));
            }

            Embedded = new PagedResultEmbedded<T>()
            {
                Data = data ?? new List<T>()
            };
            Page = new Page
            {
                Number = pageOptions.Page,
                Size = pageOptions.PageSize,
                TotalElements = totalElements,
                TotalPages = (int)Math.Ceiling((double)totalElements / (double)pageOptions.PageSize)
            };
            PageOptions = pageOptions;
            Links = new PagedResultLinks();
        }

        public PagedResult(PageOptions pageOptions, int totalElements, IEnumerable<T> data, string actionName, string controllerName, object routeValues = null)
            : this(pageOptions, totalElements, data)
        {
            Links.ActionName = actionName;
            Links.ControllerName = controllerName;
            Links.RouteValues = routeValues;
        }

        public PagedResult(PageOptions pageOptions, int totalElements, IEnumerable<T> data, string routeName, object routeValues = null)
            : this(pageOptions, totalElements, data)
        {
            Links.RouteName = routeName;
            Links.RouteValues = routeValues;
        }
    }
}
