using System;
using System.Collections.Generic;
using System.Linq;
using Digipolis.Errors;
using Digipolis.Web.Api;
using Digipolis.Web.Api.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Model;
using Swashbuckle.SwaggerGen.Generator;

namespace Digipolis.Web.Swagger
{
    public class SwaggerResponseCodeDescriptions : IOperationFilter
    {
        protected IEnumerable<Attribute> ActionAttributes { get; private set; }
        protected IEnumerable<Attribute> ControllerAttributes { get; private set; }
        protected IEnumerable<Attribute> CombinedAttributes { get; private set; }

        protected virtual string UnauthorizedDescription => "Unauthorized";
        protected virtual string ForbiddenDescription => "Forbidden";
        protected virtual string BadRequestDescription => "Bad request";
        protected virtual string InvalidModelStateDescription => "Validation failed";
        protected virtual string InternalServerErrorDescription => "Technical server error";
        protected virtual string NoContentDescription => "No content";
        protected virtual string RemovedDescription => "Removed";
        protected virtual string CreatedDescription => "Created";
        protected virtual string OkDescription => "Ok";
        protected virtual string NotFoundDescription => "Not found";

        public virtual void Apply(Operation operation, OperationFilterContext context)
        {
            ActionAttributes = context.ApiDescription.GetActionAttributes().OfType<Attribute>();
            ControllerAttributes = context.ApiDescription.GetControllerAttributes().OfType<Attribute>();
            CombinedAttributes = ActionAttributes.Union(ControllerAttributes);
            ConfigureResponses(operation, context);
        }

        protected virtual void ConfigureResponses(Operation operation, OperationFilterContext context)
        {
            UnauthorizedResponse(operation, context);
            ForbiddenResponse(operation, context);
            InternalServerErrorResponse(operation, context);
            BadRequestResponse(operation, context);
            NoContentResponse(operation, context);
            CreatedResponse(operation, context);
            OkResponse(operation, context);
            NotFoundResponse(operation, context);
            AddReturnSchemaToVersionEndpoint(operation, context);
        }

        protected virtual void UnauthorizedResponse(Operation operation, OperationFilterContext context)
        {
            ReplaceResponseDescriptionIfExists(operation, 401, UnauthorizedDescription);
        }

        protected virtual void ForbiddenResponse(Operation operation, OperationFilterContext context)
        {
            ReplaceResponseDescriptionIfExists(operation, 403, ForbiddenDescription);
        }

        protected virtual void BadRequestResponse(Operation operation, OperationFilterContext context)
        {
            if (CombinedAttributes.Any(x => x is ValidateModelStateAttribute))
                ReplaceResponseDescriptionIfExists(operation, 400, InvalidModelStateDescription);
            else
                ReplaceResponseDescriptionIfExists(operation, 400, BadRequestDescription);
        }

        protected virtual void InternalServerErrorResponse(Operation operation, OperationFilterContext context)
        {
            ReplaceResponseDescriptionIfExists(operation, 500, InternalServerErrorDescription);
        }

        protected virtual void NoContentResponse(Operation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.HttpMethod.Equals("delete", StringComparison.OrdinalIgnoreCase))
                ReplaceResponseDescriptionIfExists(operation, 204, RemovedDescription);
            else
                ReplaceResponseDescriptionIfExists(operation, 204, NoContentDescription);
        }

        protected virtual void CreatedResponse(Operation operation, OperationFilterContext context)
        {
            ReplaceResponseDescriptionIfExists(operation, 201, CreatedDescription);
        }

        protected virtual void OkResponse(Operation operation, OperationFilterContext contexts)
        {
            ReplaceResponseDescriptionIfExists(operation, 200, OkDescription);
        }

        protected virtual void NotFoundResponse(Operation operation, OperationFilterContext context)
        {
            ReplaceResponseDescriptionIfExists(operation, 404, NotFoundDescription);
        }

        protected virtual void AddReturnSchemaToVersionEndpoint(Operation operation, OperationFilterContext context)
        {
            var operationId = context.ApiDescription.RelativePath.Contains("apiVersion") ? "ByApiVersionStatusVersionGet" : "StatusVersionGet";
            if (!operation.OperationId.Equals(operationId)) return;
            operation.Responses["200"].Schema = context.SchemaRegistry.GetOrRegister(typeof(AppVersion));
            operation.Summary = "Get the version of the application";
            operation.Produces.Add("application/json");
        }

        private void ReplaceResponseDescriptionIfExists(Operation operation, int httpStatusCode, string description)
        {
            var httpStatusCodeString = httpStatusCode.ToString();
            if (operation.Responses.ContainsKey(httpStatusCodeString))
            {
                operation.Responses[httpStatusCodeString].Description = description;
            }
        }
    }
}