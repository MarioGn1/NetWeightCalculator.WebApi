using NetWeightCalculator.DTOs.Models;
using NetWeightCalculator.Services.CalculatorServices;
using NetWeightCalculator.Test.Mocks;
using System;
using Xunit;

using static NetWeightCalculator.Test.Mocks.Payer;

namespace NetWeightCalculator.Test.Services
{   
    
    public class CalculatorServiceTest
    {
        [Theory,ClassData(typeof(CalculatorTestData))]
        public void ReturnValidTaxResponceModel(
            PayerRequestModel payer, 
            JurisdictionTaxModel taxModel,
            TaxesResponseModel expectedResult)
        {
            var calculatorService = new CalculatorService();

            var actualResult = calculatorService.Calculate(payer, taxModel);

            Assert.Equal(expectedResult.GrossIncome, actualResult.GrossIncome);
            Assert.Equal(expectedResult.CharitySpent, actualResult.CharitySpent);
            Assert.Equal(expectedResult.IncomeTax, actualResult.IncomeTax);
            Assert.Equal(expectedResult.SocialTax, actualResult.SocialTax);
            Assert.Equal(expectedResult.TotalTax, actualResult.TotalTax);
        }

        [Fact]
        public void ThrowCalculationExeptions()
        {
            var calculatorService = new CalculatorService();

            Assert.Throws<InvalidOperationException>(() => 
            calculatorService.Calculate(ValidPayerDataGrossAndCharityAboveLimits, null));
        }

        [Fact]
        public void GetTaxModelExeptions()
        {
            var calculatorService = new CalculatorService();

            Assert.Throws<InvalidOperationException>(() =>
            calculatorService.GetTaxModel(null));
        }

    }
}
