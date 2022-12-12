using NetWeightCalculator.Services.Models;
using NetWeightCalculator.WebAPI.Models;
using System;

namespace NetWeightCalculator.Test.Mocks
{
    public static class Payer
    {
        public static CalculateTaxesRequestModel PayerDataGrossAndCharityAboveLimits
            => new CalculateTaxesRequestModel
            {
                SSN = "11111",
                FullName = "Test Testov",
                DateOfBirth = DateTime.Parse("1987-01-20"),
                GrossIncome = 3600,
                CharitySpent = 520
            };

        public static PayerDto ValidPayerDataGrossAndCharityAboveLimits
            => PayerDto.From("Imaginaria", "11111", "Test Testov", DateTime.Parse("1987-01-20"), 3600, 520);

        public static PayerDto ValidPayerDataGrossBellowLimits
            => PayerDto.From("Imaginaria", "11111", "Test Testov", DateTime.Parse("1987-01-20"), 1000, 520);

        public static PayerDto ValidPayerDataZeroCharity
            => PayerDto.From("Imaginaria", "11111", "Test Testov", DateTime.Parse("1987-01-20"), 1000, 0);

        public static PayerDto ValidPayerDataNullCharity
            => PayerDto.From("Imaginaria", "11111", "Test Testov", DateTime.Parse("1987-01-20"), 1000, null);

        public static PayerDto ValidPayerDataGrossAndCharityBellowLimits
            => PayerDto.From("Imaginaria", "11111", "Test Testov", DateTime.Parse("1987-01-20"), 2500, 150);

        public static PayerDto ValidPayerDataGrossBellowLimitsCharityNull
            => PayerDto.From("Imaginaria", "11111", "Test Testov", DateTime.Parse("1987-01-20"), 2500, null);
    }
}
