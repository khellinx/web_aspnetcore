using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.Web.UnitTests._TestHelpers
{
    public class TestModelMetadataProvider
    {
        // Creates a provider with all the defaults - includes data annotations
        public static IModelMetadataProvider CreateDefaultProvider()
        {
            var detailsProviders = new IMetadataDetailsProvider[]
            {
                new DefaultBindingMetadataProvider(new ModelBindingMessageProvider()),
                new DefaultValidationMetadataProvider(),
                new DataAnnotationsMetadataProvider(),
                new DataMemberRequiredBindingMetadataProvider(),
            };

            var compositeDetailsProvider = new DefaultCompositeMetadataDetailsProvider(detailsProviders);
            return new DefaultModelMetadataProvider(compositeDetailsProvider);
        }
    }
}
