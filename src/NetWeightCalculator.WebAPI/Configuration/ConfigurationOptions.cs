using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace NetWeightCalculator.WebAPI.Configuration
{
    internal static class ConfigurationOptions
    {
        internal static Action<HostBuilderContext, IConfigurationBuilder> SetUpConfiguration()
            => (hostingContext, configuration) =>
            {
                configuration.Sources.Clear();

                IHostEnvironment env = hostingContext.HostingEnvironment;

                configuration
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                    .AddJsonFile("taxrates.json", true, true);
            };
    }
}
