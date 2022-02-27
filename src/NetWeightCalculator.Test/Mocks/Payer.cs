using NetWeightCalculator.DTOs.Models;
using System;

namespace NetWeightCalculator.Test.Mocks
{
    public static class Payer
    {
        public static PayerRequestModel ValidPayerDataGrossAndCharityAboveLimits
            => new PayerRequestModel
            {
                SSN = 11111,
                FullName = "Test Testov",
                DateOfBirth = DateTime.Parse("1987-01-20"),
                GrossIncome = 3600,
                CharitySpent = 520
            };

        public static PayerRequestModel ValidPayerDataGrossBellowLimits
            => new PayerRequestModel
            {        
                GrossIncome = 1000,
                CharitySpent = 520
            };

        public static PayerRequestModel ValidPayerDataZeroCharity
            => new PayerRequestModel
            {
                GrossIncome = 1000,
                CharitySpent = 0
            };

        public static PayerRequestModel ValidPayerDataNullCharity
            => new PayerRequestModel
            {
                GrossIncome = 1000,
                CharitySpent = null
            };

        public static PayerRequestModel ValidPayerDataGrossAndCharityBellowLimits
            => new PayerRequestModel
            {
                GrossIncome = 2500,
                CharitySpent = 150
            };

        public static PayerRequestModel ValidPayerDataGrossBellowLimitsCharityNull
            => new PayerRequestModel
            {
                GrossIncome = 2500,
                CharitySpent = null
            };
    }
}
