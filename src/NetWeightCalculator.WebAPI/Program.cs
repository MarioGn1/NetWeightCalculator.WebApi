using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NetWeightCalculator.WebAPI.Configuration;

namespace NetWeightCalculator.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
            => CreateHostBuilder(args)
            .ConfigureAppConfiguration(ConfigurationOptions.SetUpConfiguration())
            .Build()
            .Run();

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder
                => webBuilder.UseStartup<Startup>());
    }
}
