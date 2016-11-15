using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digipolis.Web.UnitTests._TestHelpers
{
    public class ApiDescriptionProviderHelper
    {
        public static DefaultApiDescriptionProvider CreateDefaultApiDescriptionProvider()
        {
            var options = new MvcOptions();
            foreach (var formatter in CreateInputFormatters())
            {
                options.InputFormatters.Add(formatter);
            }

            foreach (var formatter in CreateOutputFormatters())
            {
                options.OutputFormatters.Add(formatter);
            }

            var optionsAccessor = new Mock<IOptions<MvcOptions>>();
            optionsAccessor.SetupGet(o => o.Value)
                .Returns(options);

            var constraintResolver = new Mock<IInlineConstraintResolver>();
            constraintResolver.Setup(c => c.ResolveConstraint("int"))
                .Returns(new IntRouteConstraint());

            var modelMetadataProvider = TestModelMetadataProvider.CreateDefaultProvider();

            var provider = new DefaultApiDescriptionProvider(
                optionsAccessor.Object,
                constraintResolver.Object,
                modelMetadataProvider);

            return provider;
        }

        private static List<MockInputFormatter> CreateInputFormatters()
        {
            // Include some default formatters that look reasonable, some tests will override this.
            var formatters = new List<MockInputFormatter>()
            {
                new MockInputFormatter(),
                new MockInputFormatter(),
            };

            formatters[0].SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/json"));
            formatters[0].SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/json"));

            formatters[1].SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/xml"));
            formatters[1].SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/xml"));

            return formatters;
        }

        private static List<MockOutputFormatter> CreateOutputFormatters()
        {
            // Include some default formatters that look reasonable, some tests will override this.
            var formatters = new List<MockOutputFormatter>()
            {
                new MockOutputFormatter(),
                new MockOutputFormatter(),
            };

            formatters[0].SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/json"));
            formatters[0].SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/json"));

            formatters[1].SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/xml"));
            formatters[1].SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/xml"));

            return formatters;
        }

        private class MockInputFormatter : TextInputFormatter
        {
            public List<Type> SupportedTypes { get; } = new List<Type>();

            public override Task<InputFormatterResult> ReadRequestBodyAsync(
                InputFormatterContext context,
                Encoding effectiveEncoding)
            {
                throw new NotImplementedException();
            }

            protected override bool CanReadType(Type type)
            {
                if (SupportedTypes.Count == 0)
                {
                    return true;
                }
                else if (type == null)
                {
                    return false;
                }
                else
                {
                    return SupportedTypes.Contains(type);
                }
            }
        }

        private class MockOutputFormatter : OutputFormatter
        {
            public List<Type> SupportedTypes { get; } = new List<Type>();

            public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
            {
                throw new NotImplementedException();
            }

            protected override bool CanWriteType(Type type)
            {
                if (SupportedTypes.Count == 0)
                {
                    return true;
                }
                else if (type == null)
                {
                    return false;
                }
                else
                {
                    return SupportedTypes.Contains(type);
                }
            }
        }
    }
}
