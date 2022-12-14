using System;
using System.Threading.Tasks;
using FluentAssertions;
using NetWeightCalculator.Test.Exceptions;
using NetWeightCalculator.Test.Infrastructure;
using NetWeightCalculator.WebAPI;
using NetWeightCalculator.WebAPI.Models;
using Xunit;
using static NetWeightCalculator.Test.Mocks.Payer;
using static NetWeightCalculator.Test.Mocks.CalculatorTestData;

namespace NetWeightCalculator.Test.ControllersTests
{
    public class CalculatorControllerTest : IntegrationTestBase<Program>
    {
        [Fact]
        public async Task PostCalculateShouldReturnTaxResponseModel()
        {
            var client = ApplicationFactory.CreateClient();

            var response = await client.Calculate(PayerDataGrossAndCharityAboveLimits);

            response.Should().NotBeNull();
            response!.GrossIncome.Should().Be(CalculatedTaxesGrossAndCharityAboveLimits.GrossIncome);
            response.CharitySpent.Should().Be(CalculatedTaxesGrossAndCharityAboveLimits.CharitySpent);
            response.IncomeTax.Should().Be(CalculatedTaxesGrossAndCharityAboveLimits.IncomeTax);
            response.SocialTax.Should().Be(CalculatedTaxesGrossAndCharityAboveLimits.SocialTax);
            response.TotalTax.Should().Be(CalculatedTaxesGrossAndCharityAboveLimits.TotalTax);
            response.NetIncome.Should().Be(CalculatedTaxesGrossAndCharityAboveLimits.NetIncome);
        }


        [Fact]
        public async Task PostCalculateShouldReturnBadRequestWhenPayerIsNull()
        {
            var client = ApplicationFactory.CreateClient();
            
            Func<Task> testRequest = async () => await client.Calculate();
            
            var result = await testRequest.Should().ThrowAsync<HttpResponseMessageStatusCodeInvalidException>();
            
            result.Which.ReceivedStatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("1234", "Test Testov", "1987-01-20", 3600, 520)]
        [InlineData("12345", "Test", "1987-01-20", 3600, 520)]
        [InlineData("12345", "Test Testov", "1987-01-20", 3600.001, 520)]
        [InlineData("12345", "Test Testov", "1987-01-20", 3600, -1)]
        [InlineData("12345", "Test Testov", "1987-01-20", -3600, 1)]
        [InlineData("12345123456", "Test Testov", "1987-01-20", 3600, 1)]
        public async Task PostCalculateInvalidModelState(
            string ssn,
            string fullName,
            DateTime dateOfBirth,
            decimal grossIncome,
            decimal charitySpent)
        {
            var client = ApplicationFactory.CreateClient();

            var content = new CalculateTaxesRequestModel
            {
                SSN = ssn,
                FullName = fullName,
                DateOfBirth = dateOfBirth,
                GrossIncome = grossIncome,
                CharitySpent = charitySpent
            };
            
            Func<Task> testRequest = async () => await client.Calculate(content);
            
            var result = await testRequest.Should().ThrowAsync<HttpResponseMessageStatusCodeInvalidException>();
            
            result.Which.ReceivedStatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
