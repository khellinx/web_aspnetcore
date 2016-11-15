using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Digipolis.Web.UnitTests._TestHelpers
{
    public class ActionDescriptorHelper
    {
        // Grotendeels overgenomen uit unit tests voor MVC framework zelf.
        // https://github.com/aspnet/Mvc/blob/dev/test/Microsoft.AspNetCore.Mvc.ApiExplorer.Test/DefaultApiDescriptionProviderTest.cs
        public static ControllerActionDescriptor CreateActionDescriptor(string methodName, Type controllerType)
        {
            if (string.IsNullOrEmpty(methodName)) throw new ArgumentNullException(nameof(methodName));
            if (controllerType == null) throw new ArgumentNullException(nameof(controllerType));

            var action = new ControllerActionDescriptor();
            action.SetProperty(new ApiDescriptionActionData());

            action.MethodInfo = controllerType.GetMethod(
                methodName,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            action.BoundProperties = new List<ParameterDescriptor>();
            action.ControllerTypeInfo = controllerType.GetTypeInfo();

            foreach (var property in controllerType.GetProperties())
            {
                var bindingInfo = BindingInfo.GetBindingInfo(property.GetCustomAttributes().OfType<object>());
                if (bindingInfo != null)
                {
                    action.BoundProperties.Add(new ParameterDescriptor()
                    {
                        BindingInfo = bindingInfo,
                        Name = property.Name,
                        ParameterType = property.PropertyType,
                    });
                }
            }

            action.Parameters = new List<ParameterDescriptor>();
            foreach (var parameter in action.MethodInfo.GetParameters())
            {
                action.Parameters.Add(new ParameterDescriptor()
                {
                    Name = parameter.Name,
                    ParameterType = parameter.ParameterType,
                    BindingInfo = BindingInfo.GetBindingInfo(parameter.GetCustomAttributes().OfType<object>())
                });
            }

            return action;
        }

        
    }
}
