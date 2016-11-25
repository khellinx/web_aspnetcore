using Digipolis.Web.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace Digipolis.Web.Api.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Assembly, Inherited = true, AllowMultiple = false)]
    public class PagedResultAttribute : ResultFilterAttribute, IFilterFactory
    {
        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var filterInstance = new PagedResultFilter();
            return filterInstance;
        }

        private class PagedResultFilter : IResultFilter
        {
            public void OnResultExecuted(ResultExecutedContext context)
            {
            }

            public void OnResultExecuting(ResultExecutingContext context)
            {
                // Get the original result
                var result = context.Result as ObjectResult;

                // Only apply filter when the result has an ok status and a value
                if (result?.Value == null || !result.StatusCode.HasValue || result.StatusCode != (int)HttpStatusCode.OK)
                {
                    return;
                }

                // Only apply filter if the value is of type PagedResult
                var valueType = result.Value.GetType();
                var pagedResultTypeInfo = typeof(PagedResult).GetTypeInfo();
                if (!pagedResultTypeInfo.IsAssignableFrom(valueType))
                {
                    return;
                }

                var resultValue = (PagedResult)result.Value;
                resultValue.GenerateLinks(context);
            }
        }
    }
}
