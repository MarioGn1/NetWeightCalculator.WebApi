using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetWeightCalculator.Services.Models;
using System;

namespace NetWeightCalculator.WebAPI.Configuration
{
    public static class ConfigurationOptions
    {
        public static Action<HostBuilderContext, IConfigurationBuilder> SetUpConfiguration()
            => (hostingContext, configuration) =>
            {
                configuration.Sources.Clear();

                IHostEnvironment env = hostingContext.HostingEnvironment;

                configuration
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                    .AddJsonFile("taxrates.json", true, true);
            };

        internal static IServiceCollection SetUpTaxRates(this IServiceCollection services, IConfiguration configuration)
        {
            var taxSettingsConfig = configuration.GetSection("TaxRates");
            services.Configure<TaxRates>(taxSettingsConfig);

            var taxRates = taxSettingsConfig.Get<TaxRates>();
            services.AddSingleton(taxRates);

            return services;
        }
    }
}
