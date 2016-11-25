using Digipolis.Web.Api.Tools;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.Web.Api.Models
{
    public static class PagedResultExtensions
    {
        public static PagedResult GenerateLinks(this PagedResult result, ActionContext actionContext)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            if (result.Links == null) result.Links = new PagedResultLinks();
            if (result.PageOptions == null) result.PageOptions = new PageOptions();

            var pageOptions = result.PageOptions;
            var links = result.Links;
            var routeValues = result.Links.RouteValues;

            Func<int, Link> generateLinkFunc = null;

            if (!string.IsNullOrEmpty(links.ControllerName) || !string.IsNullOrEmpty(links.ActionName))
            {
                var actionName = links.ActionName;
                var controllerName = links.ControllerName;

                generateLinkFunc = (page) => LinkProvider.GenerateLink(actionContext, pageOptions, page, actionName, controllerName, routeValues);
            }
            else
            {
                var routeName = links.RouteName;
                generateLinkFunc = (page) => LinkProvider.GenerateLink(actionContext, pageOptions, page, routeName, routeValues);
            }

            links.First = generateLinkFunc(1);
            links.Self = generateLinkFunc(pageOptions.Page);
            links.Last = generateLinkFunc(result.Page.TotalPages);

            if (pageOptions.Page > 1)
            {
                result.Links.Previous = generateLinkFunc(pageOptions.Page - 1);
            }
            if (pageOptions.Page < result.Page.TotalPages)
            {
                result.Links.Next = generateLinkFunc(pageOptions.Page + 1);
            }

            return result;
        }
    }
}
