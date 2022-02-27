using Microsoft.Extensions.Localization;
using NetWeightCalculator.DTOs.Models;
using System;
using System.Globalization;
using static NetWeightCalculator.Services.ServicesConstants.BadOperationMessages;

namespace NetWeightCalculator.Services.CalculatorServices
{
    public class CalculatorService : ICalculatorService
    {
        public TaxesResponseModel Calculate(PayerRequestModel model, JurisdictionTaxModel taxModel)
        {
            TaxesResponseModel result = new TaxesResponseModel();

            result.GrossIncome = model.GrossIncome;
            result.CharitySpent = model.CharitySpent;

            try
            {
                if (model.GrossIncome <= taxModel.TaxFreeAmount)
                {
                    result.IncomeTax = 0;
                    result.SocialTax = 0;
                    result.TotalTax = 0;
                    result.NetIncome = model.GrossIncome;
                }
                else
                {
                    decimal taxBase = GetPayerTaxBase(model.GrossIncome, model.CharitySpent, taxModel);

                    result.IncomeTax = GetIncomeTax(taxBase, taxModel);
                    result.SocialTax = GetSocialContributionsTax(taxBase, taxModel);
                    result.TotalTax = result.IncomeTax + result.SocialTax;
                    result.NetIncome = model.GrossIncome - result.TotalTax;
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException(CALCULATION_FAILED);
            }


            return result;
        }

        public JurisdictionTaxModel GetTaxModel(IStringLocalizer localizer)
        {
            try
            {
                decimal taxFreeAmount = decimal.Parse(localizer["taxFreeAmount"], CultureInfo.InvariantCulture);
                decimal incomeTax = decimal.Parse(localizer["incomeTax"], CultureInfo.InvariantCulture);
                decimal socialContributionsTax = decimal.Parse(localizer["socialContributionsTax"], CultureInfo.InvariantCulture);
                decimal socialContributionsUperLimit = decimal.Parse(localizer["socialContributionsUperLimit"], CultureInfo.InvariantCulture);
                decimal charitySpentMaxPercentage = decimal.Parse(localizer["charitySpentMaxPercentage"], CultureInfo.InvariantCulture);

                return new JurisdictionTaxModel(
                    taxFreeAmount,
                    incomeTax,
                    socialContributionsTax,
                    socialContributionsUperLimit,
                    charitySpentMaxPercentage
                );
            }
            catch (Exception)
            {
                throw new InvalidOperationException(TAX_RATES_FAILED_TO_LOAD);
            }
        }

        private decimal GetIncomeTax(decimal taxBase, JurisdictionTaxModel taxModel)
        {
            return taxBase * taxModel.IncomeTax;
        }

        private decimal GetSocialContributionsTax(decimal taxBase, JurisdictionTaxModel taxModel)
        {
            decimal socialTaxBaseMax = taxModel.SocialContributionsUperLimit - taxModel.TaxFreeAmount;

            if (taxBase > socialTaxBaseMax)
            {
                return socialTaxBaseMax * taxModel.SocialContributionsTax;
            }

            return taxBase * taxModel.SocialContributionsTax;
        }


        private decimal GetPayerTaxBase(decimal grossAmount, decimal? charitySpent, JurisdictionTaxModel jurisdictionTaxModel)
        {
            decimal taxBase = grossAmount - jurisdictionTaxModel.TaxFreeAmount;

            if (charitySpent == null || charitySpent == 0)
            {
                return taxBase;
            }

            decimal charitySpentPercentage = (decimal)charitySpent / grossAmount;

            if (charitySpentPercentage > jurisdictionTaxModel.CharitySpentMaxPercentage)
            {
                return taxBase - (grossAmount * jurisdictionTaxModel.CharitySpentMaxPercentage);
            }

            return taxBase - (decimal)charitySpent;
        }
    }
}




