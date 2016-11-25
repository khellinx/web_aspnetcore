using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

namespace Digipolis.Web.Api.Tools
{
    // TODO: This shouldn't be a static class
    internal static class LinkProvider
    {
        // This shouldn't be necessary but it is kept here to be backwards compatible.
        private static IActionContextAccessor _httpContextAccessor;

        internal static void Configure(IActionContextAccessor httpContextAccessor)
        {
            //TODO: add null check!
            _httpContextAccessor = httpContextAccessor;
        }

        internal static Link GenerateLink(ActionContext actionContext, PageOptions pageOptions, int page, string actionName, string controllerName, object routeValues = null)
        {
            // This shouldn't be necessary but it is kept here to be backwards compatible.
            if (actionContext == null)
            {
                actionContext = _httpContextAccessor.ActionContext;
            }

            var values = GetRouteValues(pageOptions, page, routeValues);

            var url = AbsoluteAction(actionContext, actionName, controllerName, values);
            return new Link(url.ToLowerInvariant());
        }

        internal static Link GenerateLink(ActionContext actionContext, PageOptions pageOptions, int page, string routeName, object routeValues = null)
        {
            // This shouldn't be necessary but it is kept here to be backwards compatible.
            if (actionContext == null)
            {
                actionContext = _httpContextAccessor.ActionContext;
            }

            var values = GetRouteValues(pageOptions, page, routeValues);

            var url = AbsoluteRoute(actionContext, routeName, values);
            return new Link(url.ToLowerInvariant());
        }

        private static RouteValueDictionary GetRouteValues(PageOptions pageOptions, int page, object routeValues)
        {
            var values = new RouteValueDictionary(routeValues);

            values.Add("Page", page);
            values.Add("PageSize", pageOptions.PageSize);

            var pageSortOptions = pageOptions as PageSortOptions;
            if (pageSortOptions?.Sort != null && pageSortOptions.Sort.Length > 0)
            {
                values.Add("Sort", pageSortOptions.Sort);
            }

            return values;
        }

        private static string AbsoluteAction(ActionContext actionContext, string actionName, string controllerName, object routeValues = null)
        {
            string scheme = actionContext.HttpContext.Request.Scheme;
            var helper = new UrlHelper(actionContext);
            return helper.Action(actionName, controllerName, routeValues, scheme);
        }

        private static string AbsoluteRoute(ActionContext actionContext, string routeName, object routeValues = null)
        {
            var helper = new UrlHelper(actionContext);
            return helper.Link(routeName, routeValues);
        }
    }
}
