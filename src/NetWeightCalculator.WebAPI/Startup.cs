using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NetWeightCalculator.Services.CalculatorServices;
using NetWeightCalculator.Services.MemoryCacheServices;
using NetWeightCalculator.WebAPI.Configuration;
using NetWeightCalculator.WebAPI.Models.Validators;

namespace NetWeightCalculator.WebAPI
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .SetUpTaxRates(_configuration)
                .AddControllers();

            services
                .AddFluentValidationAutoValidation()
                .AddValidatorsFromAssemblyContaining<PayerRequestModelValidator>()
                .AddSwaggerGen(c =>
                    {
                        c.SwaggerDoc("v1", new OpenApiInfo { Title = "NetWeightCalculator.WebAPI", Version = "v1" });
                    })
                .AddMemoryCache();

            services
                .AddTransient<ICalculatorService, CalculatorService>()
                .AddTransient<ICalculatorCacheService, CalculatorCacheService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NetWeightCalculator.WebAPI v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
