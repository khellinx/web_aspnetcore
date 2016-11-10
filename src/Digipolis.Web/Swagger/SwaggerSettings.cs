using System.IO;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.SwaggerGen.Application;

namespace Digipolis.Web.Swagger
{
    public abstract class SwaggerSettings<TSwaggerResponseCodeDescriptions> where TSwaggerResponseCodeDescriptions : SwaggerResponseCodeDescriptions
    {
        public void Configure(SwaggerGenOptions options)
        {
            // Add the operation filter for the Swagger response mappings
            options.OperationFilter<TSwaggerResponseCodeDescriptions>();

            // Include Xml comments
            var xmlPath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, PlatformServices.Default.Application.ApplicationName + ".xml");
            if (File.Exists(xmlPath)) options.IncludeXmlComments(xmlPath);

            // File upload parameters operation filter
            options.OperationFilter<AddFileUploadParams>();

            // Set version in paths document filter
            options.DocumentFilter<SetVersionInPaths>();

            Configuration(options);
        }

        protected abstract void Configuration(SwaggerGenOptions options);
    }
}