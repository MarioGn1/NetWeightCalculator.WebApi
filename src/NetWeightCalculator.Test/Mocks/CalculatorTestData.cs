using NetWeightCalculator.DTOs.Models;
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
                ValidJurisdictionTaxModel, 
                CalculatedTaxesGrossAndCharityAboveLimits 
            };
            yield return new object[]
            {
                ValidPayerDataGrossBellowLimits,
                ValidJurisdictionTaxModel,
                CalculatedTaxesGrossBellowLimit 
            };
            yield return new object[]
            {
                ValidPayerDataZeroCharity,
                ValidJurisdictionTaxModel,
                CalculatedTaxesZeroCharity
            };
            yield return new object[]
            {
                ValidPayerDataNullCharity,
                ValidJurisdictionTaxModel,
                CalculatedTaxesNullCharity
            };
            yield return new object[]
            {
                ValidPayerDataGrossAndCharityBellowLimits,
                ValidJurisdictionTaxModel,
                CalculatedTaxesGrossAndCharityBellowLimits
            };
            yield return new object[]
            {
                ValidPayerDataGrossBellowLimitsCharityNull,
                ValidJurisdictionTaxModel,
                CalculatedTaxesGrossBellowLimitsCharityNull
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static TaxesResponseModel CalculatedTaxesGrossAndCharityAboveLimits
           => new TaxesResponseModel
           {
               GrossIncome = 3600M,
               CharitySpent = 520M,
               IncomeTax = 224.00M,
               SocialTax = 300.00M,
               TotalTax = 524.00M,
               NetIncome = 3076.00M
           };

        public static TaxesResponseModel CalculatedTaxesGrossAndCharityBellowLimits
            => new TaxesResponseModel
            {
                GrossIncome = 2500M,
                CharitySpent = 150M,
                IncomeTax = 135.00M,
                SocialTax = 202.50M,
                TotalTax = 337.50M,
                NetIncome = 2162.50M
            };

        public static TaxesResponseModel CalculatedTaxesGrossBellowLimitsCharityNull
            => new TaxesResponseModel
            {
                GrossIncome = 2500M,
                CharitySpent = null,
                IncomeTax = 150.00M,
                SocialTax = 225.00M,
                TotalTax = 375.00M,
                NetIncome = 2125.00M
            };

        public static TaxesResponseModel CalculatedTaxesGrossBellowLimit
            => new TaxesResponseModel
            {
                GrossIncome = 1000M,
                CharitySpent = 520M,
                IncomeTax = 0,
                SocialTax = 0,
                TotalTax = 0,
                NetIncome = 1000M
            };

        public static TaxesResponseModel CalculatedTaxesZeroCharity
            => new TaxesResponseModel
            {
                GrossIncome = 1000M,
                CharitySpent = 0,
                IncomeTax = 0,
                SocialTax = 0,
                TotalTax = 0,
                NetIncome = 1000M
            };

        public static TaxesResponseModel CalculatedTaxesNullCharity
            => new TaxesResponseModel
            {
                GrossIncome = 1000M,
                CharitySpent = null,
                IncomeTax = 0,
                SocialTax = 0,
                TotalTax = 0,
                NetIncome = 1000M
            };

        public static JurisdictionTaxModel ValidJurisdictionTaxModel
            => new JurisdictionTaxModel(1000, 0.1M, 0.15M, 3000, 0.1M);
            
    }
}
