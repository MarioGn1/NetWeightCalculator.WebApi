using NetWeightCalculator.DTOs.Models;
using System;

namespace NetWeightCalculator.Test.Mocks
{
    public static class Payer
    {
        public static PayerRequestModel ValidPayerData
            => new PayerRequestModel
            {
                SSN = 11111,
                FullName = "Test Testov",
                DateOfBirth = DateTime.Parse("1987-01-20"),
                GrossIncome = 3600,
                CharitySpent = 520
            };
    }
}
