﻿using System;
using System.Linq;
using Digipolis.Web.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Digipolis.Web.UnitTests.Startup
{
    public class MvcBuilderExtensionsTests
    {
        [Fact]
        private void WebVersioningOptionsSetupIsRegisteredAsTransient()
        {
            var services = new ServiceCollection();
            var manager = new ApplicationPartManager();
            var builder = new MvcBuilder(services, manager);
            
            builder.AddVersionEndpoint();

            var registrations = services.Where(sd => sd.ServiceType == typeof(IConfigureOptions<MvcOptions>)
                                               && sd.ImplementationType == typeof(WebVersioningOptionsSetup))
                                        .ToArray();
            Assert.Equal(1, registrations.Count());
            Assert.Equal(ServiceLifetime.Transient, registrations[0].Lifetime);
        }

        [Fact]
        private void WebVersioningOptionsIsRegisteredAsSingleton()
        {
            var services = new ServiceCollection();
            var manager = new ApplicationPartManager();
            var builder = new MvcBuilder(services, manager);

            builder.AddVersionEndpoint(options => options.Route = "myroute");

            var registrations = services.Where(sd => sd.ServiceType == typeof(IConfigureOptions<WebVersioningOptions>)).ToArray();
            Assert.Equal(1, registrations.Count());
            Assert.Equal(ServiceLifetime.Singleton, registrations[0].Lifetime);
        }

        [Fact]
        private void WebVersionProviderIsRegisteredAsSingleton()
        {
            var services = new ServiceCollection();
            var manager = new ApplicationPartManager();
            var builder = new MvcBuilder(services, manager);

            builder.AddVersionEndpoint();

            var registrations = services.Where(sd => sd.ServiceType == typeof(IVersionProvider)
                                               && sd.ImplementationType == typeof(WebVersionProvider))
                                        .ToArray();
            Assert.Equal(1, registrations.Count());
            Assert.Equal(ServiceLifetime.Singleton, registrations[0].Lifetime);
        }

    }
}
