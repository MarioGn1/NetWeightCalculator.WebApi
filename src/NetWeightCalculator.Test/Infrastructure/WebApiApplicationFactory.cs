using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetWeightCalculator.WebAPI.Configuration;

namespace NetWeightCalculator.Test.Infrastructure
{
    public class WebApiApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint>
        where TEntryPoint : class
    {
        private static readonly Dictionary<Type, object> _applicationFactories = new();

        private readonly Action<IServiceCollection> _registerServices;

        internal static WebApiApplicationFactory<TEntryPoint> Create(Action<IServiceCollection> registerServicesAction,
            Action<WebApiApplicationFactory<TEntryPoint>> onCreationAction)
        {
            var applicationFactoryType = typeof(WebApiApplicationFactory<TEntryPoint>);

            if (_applicationFactories.ContainsKey(applicationFactoryType))
                return (WebApiApplicationFactory<TEntryPoint>)_applicationFactories[applicationFactoryType];

            var applicationFactory = new WebApiApplicationFactory<TEntryPoint>(registerServicesAction);

            _applicationFactories.Add(applicationFactoryType, applicationFactory);

            // start the api server initialization,
            // so when the real test executes the server should be already running
            var _ = applicationFactory.Server;

            onCreationAction(applicationFactory);

            return applicationFactory;
        }

        private WebApiApplicationFactory(Action<IServiceCollection> registerServices)
        {
            _registerServices = registerServices;
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(ConfigurationOptions.SetUpConfiguration());
            
            builder.ConfigureServices((_, services) =>
            {
                _registerServices(services);
            });

            builder.UseEnvironment(Environments.Staging);

            return base.CreateHost(builder);
        }
    }
}

