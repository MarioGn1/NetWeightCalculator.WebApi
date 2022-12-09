using NetWeightCalculator.Services.Models;
using System;
using System.Linq;
using static NetWeightCalculator.Services.ServicesConstants.BadOperationMessages;

namespace NetWeightCalculator.Services.CalculatorServices
{
    public class CalculatorService : ICalculatorService
    {
        private readonly TaxRates _taxRates;

        public CalculatorService(TaxRates taxRates)
        {
            _taxRates = taxRates;
        }

        public TaxesDto Calculate(PayerDto model)
        {
            var requiredTaxes = _taxRates.Jurisdictions.FirstOrDefault(x => x.Country == model.Country);

            try
            {
                if (model.GrossIncome <= requiredTaxes.TaxFreeAmount)
                {
                    return TaxesDto.NoCharges(model.GrossIncome, model.CharitySpent, model.GrossIncome);
                }
                else
                {
                    decimal taxBase = GetPayerTaxBase(model.GrossIncome, model.CharitySpent, requiredTaxes);

                    var incomeTax = GetIncomeTax(taxBase, requiredTaxes);
                    var socialTax = GetSocialContributionsTax(taxBase, requiredTaxes);
                    var totalTax = incomeTax + socialTax;
                    var netIncome = model.GrossIncome - totalTax;

                    return TaxesDto.ApplyCharges(
                        model.GrossIncome,
                        model.CharitySpent,
                        netIncome,
                        incomeTax,
                        socialTax,
                        totalTax);
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException(CALCULATION_FAILED);
            }
        }

        private decimal GetIncomeTax(decimal taxBase, Jurisdiction taxModel)
        {
            return taxBase * taxModel.IncomeTax;
        }

        private decimal GetSocialContributionsTax(decimal taxBase, Jurisdiction taxModel)
        {
            decimal socialTaxBaseMax = taxModel.SocialContributionsUperLimit - taxModel.TaxFreeAmount;

            if (taxBase > socialTaxBaseMax)
            {
                return socialTaxBaseMax * taxModel.SocialContributionsTax;
            }

            return taxBase * taxModel.SocialContributionsTax;
        }


        private decimal GetPayerTaxBase(decimal grossAmount, decimal? charitySpent, Jurisdiction jurisdictionTaxModel)
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




