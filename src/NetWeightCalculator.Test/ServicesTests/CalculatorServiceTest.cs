using System;
using Microsoft.Extensions.DependencyInjection;
using NetWeightCalculator.Services.CalculatorServices;
using NetWeightCalculator.Services.Models;
using NetWeightCalculator.Test.Infrastructure;
using NetWeightCalculator.Test.Mocks;
using NetWeightCalculator.WebAPI;
using NetWeightCalculator.WebAPI.Models;
using Xunit;
using static NetWeightCalculator.Test.Mocks.Payer;

namespace NetWeightCalculator.Test.ServicesTests
{
    public class CalculatorServiceTest : IntegrationTestBase<Program>
    {
        [Theory, ClassData(typeof(CalculatorTestData))]
        public void ReturnValidTaxResponseModel(PayerDto payer, CalculateTaxesResponseModel expectedResult)
        {
            var calculatorService = ApplicationFactory.Services.GetRequiredService<ICalculatorService>();
            var actualResult = calculatorService.Calculate(payer);

            Assert.Equal(expectedResult.GrossIncome, actualResult.GrossIncome);
            Assert.Equal(expectedResult.CharitySpent, actualResult.CharitySpent);
            Assert.Equal(expectedResult.IncomeTax, actualResult.IncomeTax);
            Assert.Equal(expectedResult.SocialTax, actualResult.SocialTax);
            Assert.Equal(expectedResult.TotalTax, actualResult.TotalTax);
        }

        [Fact]
        public void ThrowCalculationExceptions()
        {
            var calculatorService = ApplicationFactory.Services.GetRequiredService<ICalculatorService>();
            Assert.Throws<ArgumentException>(() =>
                calculatorService.Calculate(InvalidPayerDataGrossAndCharityAboveLimits));
        }
    }
}