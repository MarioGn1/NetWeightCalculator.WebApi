using NetWeightCalculator.Services.MemoryCacheServices;
using NetWeightCalculator.Services.Models;
using System.Linq;
using System.Reflection;

namespace NetWeightCalculator.Services.CalculatorServices
{
    public class CalculatorService : ICalculatorService
    {
        private readonly TaxRates _taxRates;
        private readonly ICalculatorCacheService calculatorCache;

        public CalculatorService(TaxRates taxRates, ICalculatorCacheService calculatorCache)
        {
            _taxRates = taxRates;
            this.calculatorCache = calculatorCache;
        }

        public TaxesDto Calculate(PayerDto model)
        {
            var requiredTaxes = _taxRates.Jurisdictions.FirstOrDefault(x => x.Country == model.Country);

            var identityKey = IdentityCacheKey.From(model);
            var taxesKey = TaxesCalculationCacheKey.From(model);

            if (!calculatorCache.IsExistentPayer(identityKey))
            {
                calculatorCache.SetPayerIdentity(identityKey);
                return CalculateAndCache(model, requiredTaxes, taxesKey);
            }

            var taxes = calculatorCache.GetTaxes(taxesKey);

            if (taxes == null)
            {
                taxes = CalculateAndCache(model, requiredTaxes, taxesKey);
            }

            return taxes;
        }

        private TaxesDto CalculateAndCache(PayerDto model, Jurisdiction requiredTaxes, TaxesCalculationCacheKey taxesKey)
        {
            var taxes = NewCalculation(model, requiredTaxes);

            calculatorCache.SetTaxes(taxesKey, taxes);

            return taxes;
        }

        private TaxesDto NewCalculation(PayerDto model, Jurisdiction requiredTaxes)
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




