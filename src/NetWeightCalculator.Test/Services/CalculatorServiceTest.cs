using NetWeightCalculator.Services.CalculatorServices;
using NetWeightCalculator.Services.Models;
using NetWeightCalculator.Test.Mocks;
using NetWeightCalculator.WebAPI.Models;
using System;
using Xunit;

using static NetWeightCalculator.Test.Mocks.Payer;

namespace NetWeightCalculator.Test.Services
{

    public class CalculatorServiceTest
    {
        private readonly ICalculatorService _calculatorService;

        public CalculatorServiceTest(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        [Theory,ClassData(typeof(CalculatorTestData))]
        public void ReturnValidTaxResponceModel(
            PayerDto payer,
            CalculateTaxesResponseModel expectedResult)
        {
            var actualResult = _calculatorService.Calculate(payer);

            Assert.Equal(expectedResult.GrossIncome, actualResult.GrossIncome);
            Assert.Equal(expectedResult.CharitySpent, actualResult.CharitySpent);
            Assert.Equal(expectedResult.IncomeTax, actualResult.IncomeTax);
            Assert.Equal(expectedResult.SocialTax, actualResult.SocialTax);
            Assert.Equal(expectedResult.TotalTax, actualResult.TotalTax);
        }

        [Fact]
        public void ThrowCalculationExeptions()
        {
            Assert.Throws<InvalidOperationException>(() =>
            _calculatorService.Calculate(ValidPayerDataGrossAndCharityAboveLimits));
        }
    }
}
