using NetWeightCalculator.WebAPI.Models;
using System.Collections;
using System.Collections.Generic;

using static NetWeightCalculator.Test.Mocks.Payer;

namespace NetWeightCalculator.Test.Mocks
{
    public class CalculatorTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                ValidPayerDataGrossAndCharityAboveLimits,
                CalculatedTaxesGrossAndCharityAboveLimits
            };
            yield return new object[]
            {
                ValidPayerDataGrossBellowLimits,
                CalculatedTaxesGrossBellowLimit
            };
            yield return new object[]
            {
                ValidPayerDataZeroCharity,
                CalculatedTaxesZeroCharity
            };
            yield return new object[]
            {
                ValidPayerDataZeroCharity,
                CalculatedTaxesNullCharity
            };
            yield return new object[]
            {
                ValidPayerDataGrossAndCharityBellowLimits,
                CalculatedTaxesGrossAndCharityBellowLimits
            };
            yield return new object[]
            {
                ValidPayerDataGrossBellowLimitsZeroCharity,
                CalculatedTaxesGrossBellowLimitsCharityNull
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static CalculateTaxesResponseModel CalculatedTaxesGrossAndCharityAboveLimits
           => new CalculateTaxesResponseModel(3600M, 520M, 224.00M, 300.00M, 524.00M, 3076.00M);

        public static CalculateTaxesResponseModel CalculatedTaxesGrossAndCharityBellowLimits
            => new CalculateTaxesResponseModel(2500M, 150M, 135.00M, 202.50M, 337.50M, 2162.50M);

        public static CalculateTaxesResponseModel CalculatedTaxesGrossBellowLimitsCharityNull
            => new CalculateTaxesResponseModel(2500M, 0, 150.00M, 225.00M, 375.00M, 2125.00M);

        public static CalculateTaxesResponseModel CalculatedTaxesGrossBellowLimit
            => new CalculateTaxesResponseModel(1000M, 520M, 0, 0, 0, 1000M);

        public static CalculateTaxesResponseModel CalculatedTaxesZeroCharity
            => new CalculateTaxesResponseModel(1000M, 0, 0, 0, 0, 1000M);

        public static CalculateTaxesResponseModel CalculatedTaxesNullCharity
            => new CalculateTaxesResponseModel(1000M, 0, 0, 0, 0, 1000M);
    }
}
