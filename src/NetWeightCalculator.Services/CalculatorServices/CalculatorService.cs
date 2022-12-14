using NetWeightCalculator.Services.MemoryCacheServices;
using NetWeightCalculator.Services.Models;
using System.Linq;

namespace NetWeightCalculator.Services.CalculatorServices
{
    public class CalculatorService : ICalculatorService
    {
        private readonly TaxRates _taxRates;
        private readonly ICalculatorCacheService _calculatorCache;

        public CalculatorService(TaxRates taxRates, ICalculatorCacheService calculatorCache)
        {
            _taxRates = taxRates;
            _calculatorCache = calculatorCache;
        }

        public TaxesDto Calculate(PayerDto model)
        {
            var requiredTaxes = _taxRates.Jurisdictions.FirstOrDefault(x => x.Country == model.Country);

            var identityKey = IdentityCacheKey.From(model);
            var taxesKey = TaxesCalculationCacheKey.From(model);

            if (!_calculatorCache.IsExistentPayer(identityKey))
            {
                _calculatorCache.SetPayerIdentity(identityKey);
                return CalculateAndCache(model, requiredTaxes, taxesKey);
            }

            var taxes = _calculatorCache.GetTaxes(taxesKey);

            if (taxes == null)
            {
                taxes = CalculateAndCache(model, requiredTaxes, taxesKey);
            }

            return taxes;
        }

        private TaxesDto CalculateAndCache(PayerDto model, Jurisdiction requiredTaxes,
            TaxesCalculationCacheKey taxesKey)
        {
            var taxes = NewCalculation(model, requiredTaxes);

            _calculatorCache.SetTaxes(taxesKey, taxes);

            return taxes;
        }

        private static TaxesDto NewCalculation(PayerDto model, Jurisdiction requiredTaxes)
        {
            if (model.GrossIncome <= requiredTaxes.TaxFreeAmount)
            {
                return TaxesDto.NoCharges(model.GrossIncome, model.CharitySpent, model.GrossIncome);
            }

            var taxBase = GetPayerTaxBase(model.GrossIncome, model.CharitySpent, requiredTaxes);

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

        private static decimal GetIncomeTax(decimal taxBase, Jurisdiction taxModel)
        {
            return taxBase * taxModel.IncomeTax;
        }

        private static decimal GetSocialContributionsTax(decimal taxBase, Jurisdiction taxModel)
        {
            var socialTaxBaseMax = taxModel.SocialContributionsUperLimit - taxModel.TaxFreeAmount;

            if (taxBase > socialTaxBaseMax)
            {
                return socialTaxBaseMax * taxModel.SocialContributionsTax;
            }

            return taxBase * taxModel.SocialContributionsTax;
        }

        private static decimal GetPayerTaxBase(decimal grossAmount, decimal? charitySpent,
            Jurisdiction jurisdictionTaxModel)
        {
            var taxBase = grossAmount - jurisdictionTaxModel.TaxFreeAmount;

            if (charitySpent == null || charitySpent == 0)
            {
                return taxBase;
            }

            var charitySpentPercentage = (decimal)charitySpent / grossAmount;

            if (charitySpentPercentage > jurisdictionTaxModel.CharitySpentMaxPercentage)
            {
                return taxBase - (grossAmount * jurisdictionTaxModel.CharitySpentMaxPercentage);
            }

            return taxBase - (decimal)charitySpent;
        }
    }
}