using Xunit;
using MyTested.AspNetCore.Mvc;
using NetWeightCalculator.WebAPI.Controllers;
using NetWeightCalculator.DTOs.Models;

using static NetWeightCalculator.Test.Mocks.Payer;
using System;

namespace NetWeightCalculator.Test.Controllers
{    
    public class CalculatorControllerTest
    {
        [Fact]
        public void PostCalculateShouldReturnTaxResponceModel()
            => MyController<CalculatorController>
            .Instance()
            .Calling(x => x.Calculate(ValidPayerData))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post))
            .ValidModelState()
            .AndAlso()
            .ShouldReturn()
            .Object(x => 
                x.WithModelOfType<TaxesResponseModel>());

        [Fact]
        public void PostCalculateShouldReturnBadRequestWhenPayerIsNull()
            => MyController<CalculatorController>
            .Instance()
            .Calling(x => x.Calculate(null))
            .ShouldReturn()
            .BadRequest();

        [Theory]
        [InlineData(1234, "Test Testov", "1987-01-20", 3600, 520)]
        [InlineData(12345, "Test", "1987-01-20", 3600, 520)]
        [InlineData(12345, "Test Testov", "1987-01-20", 3600.001, 520)]
        [InlineData(12345, "Test Testov", "1987-01-20", 3600, -1)]
        public void PostCalculateShouldReturnBadRequestWhenPayerWrongData(
            int ssn,
            string fullName,
            DateTime dateofBirth,
            decimal grossIncome,
            decimal charitySpent)
            => MyController<CalculatorController>
            .Instance()
            .Calling(x =>
                x.Calculate(new PayerRequestModel
                {
                    SSN = ssn,
                    FullName = fullName,
                    DateOfBirth = dateofBirth,
                    GrossIncome = grossIncome,
                    CharitySpent = charitySpent
                }))
            .ShouldHave()
            .InvalidModelState();
    }
}
