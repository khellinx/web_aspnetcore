using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Digipolis.Web.Api.ApiExplorer
{
    public static class ActionDescriptorExtensions
    {
        public static bool HasCustomControllerActionAttribute<TAttribute>(this ActionDescriptor actionDescriptor)
            where TAttribute : Attribute
        {
            var controllerActionDescriptor = actionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor == null) return false;

            var result = controllerActionDescriptor.MethodInfo.CustomAttributes.Where(x => x.AttributeType == typeof(TAttribute)).Any();

            return result;
        }

        public static TAttribute GetCustomControllerActionAttribute<TAttribute>(this ActionDescriptor actionDescriptor)
            where TAttribute : Attribute
        {
            var controllerActionDescriptor = actionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor == null) return null;

            var result = controllerActionDescriptor.MethodInfo.GetCustomAttribute(typeof(TAttribute), false);

            return (TAttribute)result;
        }
    }
}
