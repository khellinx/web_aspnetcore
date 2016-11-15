using Digipolis.Web.Api;
using Digipolis.Web.UnitTests._TestHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.Web.UnitTests._TestFixtures
{
    public class ApiDescriptionProviderContextCrudControllerFixture
    {
        public ApiDescriptionProviderContext CreateContext(bool applyDefaultApiDescriptionProvider = true)
        {
            var getAllActionDescriptor = ActionDescriptorHelper.CreateActionDescriptor("GetAll", typeof(ValuesController));
            var getActionDescriptor = ActionDescriptorHelper.CreateActionDescriptor("Get", typeof(ValuesController));
            var addActionDescriptor = ActionDescriptorHelper.CreateActionDescriptor("Add", typeof(ValuesController));
            var updateActionDescriptor = ActionDescriptorHelper.CreateActionDescriptor("Update", typeof(ValuesController));
            var deleteActionDescriptor = ActionDescriptorHelper.CreateActionDescriptor("Delete", typeof(ValuesController));

            var actionDescriptorList = new List<ActionDescriptor>(new[] {
                getAllActionDescriptor,
                getActionDescriptor,
                addActionDescriptor,
                updateActionDescriptor,
                deleteActionDescriptor
            }).AsReadOnly();
            var context = new ApiDescriptionProviderContext(actionDescriptorList);

            if (applyDefaultApiDescriptionProvider)
            {
                var defaultApiDescriptionProvider = ApiDescriptionProviderHelper.CreateDefaultApiDescriptionProvider();
                defaultApiDescriptionProvider.OnProvidersExecuting(context);
            }

            return context;
        }

        private class ValueModel
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string Description { get; set; }
        }

        [Route("api/[controller]")]
        private class ValuesController : Controller
        {
            [HttpGet()]
            [ProducesResponseType(typeof(ValueModel), 200)]
            [ProducesResponseType(typeof(Errors.Error), 500)]
            public IActionResult GetAll([FromQuery] PageOptions queryOptions)
            {
                throw new NotImplementedException();
            }

            [HttpGet("{id}")]
            [ProducesResponseType(typeof(ValueModel), 200)]
            [ProducesResponseType(typeof(Errors.Error), 404)]
            [ProducesResponseType(typeof(Errors.Error), 500)]
            public IActionResult Get([FromRoute] int id)
            {
                throw new NotImplementedException();
            }

            [HttpPost()]
            [ProducesResponseType(typeof(ValueModel), 201)]
            [ProducesResponseType(typeof(Errors.Error), 500)]
            public IActionResult Add([FromBody] ValueModel value)
            {
                throw new NotImplementedException();
            }

            [HttpPut("{id}")]
            [ProducesResponseType(typeof(ValueModel), 200)]
            [ProducesResponseType(typeof(Errors.Error), 404)]
            [ProducesResponseType(typeof(Errors.Error), 500)]
            public IActionResult Update([FromRoute] int id, [FromBody] ValueModel value)
            {
                throw new NotImplementedException();
            }

            [HttpDelete("{id}")]
            [ProducesResponseType(typeof(ValueModel), 204)]
            [ProducesResponseType(typeof(Errors.Error), 404)]
            [ProducesResponseType(typeof(Errors.Error), 500)]
            public IActionResult Delete([FromRoute] int id)
            {
                throw new NotImplementedException();
            }
        }
    }
}
