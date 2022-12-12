using System;
using Xunit;
using MyTested.AspNetCore.Mvc;
using static NetWeightCalculator.Test.Mocks.Payer;
using NetWeightCalculator.WebAPI.Models;
using NetWeightCalculator.WebAPI.Controllers;

namespace NetWeightCalculator.Test.Controllers
{
    public class CalculatorControllerTest
    {
        [Fact]
        public void PostCalculateShouldReturnTaxResponceModel()
            => MyController<CalculatorController>
            .Instance()
            .Calling(x => x.Calculate(PayerDataGrossAndCharityAboveLimits))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post))
            .ValidModelState()
            .AndAlso()
            .ShouldReturn()
            .Object(x =>
                x.WithModelOfType<CalculateTaxesResponseModel>());

        [Fact]
        public void PostCalculateShouldReturnBadRequestWhenPayerIsNull()
            => MyController<CalculatorController>
            .Instance()
            .Calling(x => x.Calculate(null))
            .ShouldReturn()
            .BadRequest();

        [Theory]
        [InlineData("1234", "Test Testov", "1987-01-20", 3600, 520)]
        [InlineData("12345", "Test", "1987-01-20", 3600, 520)]
        [InlineData("12345", "Test Testov", "1987-01-20", 3600.001, 520)]
        [InlineData("12345", "Test Testov", "1987-01-20", 3600, -1)]
        public void PostCalculateInvalidModelState(
            string ssn,
            string fullName,
            DateTime dateofBirth,
            decimal grossIncome,
            decimal charitySpent)
            => MyController<CalculatorController>
            .Instance()
            .Calling(x =>
                x.Calculate(new CalculateTaxesRequestModel
                {
                    SSN = ssn,
                    FullName = fullName,
                    DateOfBirth = dateofBirth,
                    GrossIncome = grossIncome,
                    CharitySpent = charitySpent
                }))
            .ShouldHave()
            .InvalidModelState();

        [Theory]
        [InlineData("12345", "Test Testov", "1987-01-20", 3600, 520)]
        public void PostCalculateAlreadyExistOrWrongInput(
            string ssn,
            string fullName,
            DateTime dateofBirth,
            decimal grossIncome,
            decimal charitySpent)
            => MyController<CalculatorController>
            .Instance()
            .WithMemoryCache(cache => cache
                .WithEntry(ssn, new CalculateTaxesRequestModel
                {
                    SSN = ssn,
                    FullName = fullName,
                    DateOfBirth = dateofBirth,
                    GrossIncome = grossIncome,
                    CharitySpent = charitySpent
                }))
            .Calling(x =>
                x.Calculate(new CalculateTaxesRequestModel
                {
                    SSN = ssn,
                    FullName = "Test Testo",
                    DateOfBirth = dateofBirth,
                    GrossIncome = grossIncome,
                    CharitySpent = charitySpent
                }))
            .ShouldHave()
            .MemoryCache(cache => cache
                .ContainingEntry(entry => entry.WithKey(ssn)))
            .AndAlso()
            .ShouldReturn()
            .BadRequest();
    }
}
