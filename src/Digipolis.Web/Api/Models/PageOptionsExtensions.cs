using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digipolis.Web.Api.Tools;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

namespace Digipolis.Web.Api.Models
{
    public static class PageOptionsExtensions
    {
        public static PagedResult<T> ToPagedResult<T>(this PageOptions pageOptions, IEnumerable<T> data, int total, string actionName, string controllerName, object routeValues = null) where T : class, new()
        {
            if (string.IsNullOrWhiteSpace(actionName)) throw new ArgumentNullException(nameof(actionName));
            if (string.IsNullOrWhiteSpace(controllerName)) throw new ArgumentNullException(nameof(controllerName));

            var result = new PagedResult<T>(pageOptions, total, data, actionName, controllerName, routeValues);

            return result;
        }

        public static PagedResult<T> ToPagedResult<T>(this PageOptions pageOptions, IEnumerable<T> data, int total, string routeName, object routeValues = null) where T : class, new()
        {
            if (string.IsNullOrWhiteSpace(routeName)) throw new ArgumentNullException(nameof(routeName));

            var result = new PagedResult<T>(pageOptions, total, data, routeName, routeValues);

            return result;
        }
    }
}
