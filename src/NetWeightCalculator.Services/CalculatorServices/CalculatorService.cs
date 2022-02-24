using NetWeightCalculator.DTOs;

namespace NetWeightCalculator.Services.CalculatorServices
{
    public class CalculatorService : ICalculatorService
    {
        private const decimal taxFreeAmount = 1000;
        private const decimal incomeTax = 0.1M;
        private const decimal socialContributionsTax = 0.15M;
        private const decimal socialContributionsUperLimit = 3000;
        private const decimal charitySpentMaxPercentage = 0.1M;


        public TaxesResponseModel Calculate(PayerRequestModel model)
        {
            TaxesResponseModel result = new TaxesResponseModel();            
            
            if (model.GrossIncome <= taxFreeAmount)
            {
                result.GrossIncome = model.GrossIncome;
                result.CharitySpent = model.CharitySpent;
                result.IncomeTax = 0;
                result.SocialTax = 0;
                result.TotalTax = 0;
                result.NetIncome = model.GrossIncome;
            }
            else
            {
                decimal taxBase = GetPayerTaxBase(model.GrossIncome, model.CharitySpent);

                result.GrossIncome = model.GrossIncome;
                result.CharitySpent = model.CharitySpent;
                result.IncomeTax = GetIncomeTax(taxBase);
                result.SocialTax = GetSocialContributionsTax(taxBase);
                result.TotalTax = result.IncomeTax + result.SocialTax;
                result.NetIncome = model.GrossIncome - result.TotalTax;
            }

            return result;
        }

        private decimal GetIncomeTax(decimal taxBase)
        {
            return taxBase * incomeTax;
        }

        private decimal GetSocialContributionsTax(decimal taxBase)
        {
            decimal socialTaxBaseMax = socialContributionsUperLimit - taxFreeAmount;

            if (taxBase > socialTaxBaseMax)
            {
                return socialTaxBaseMax * socialContributionsTax;
            }

            return taxBase * socialContributionsTax;
        }


        private decimal GetPayerTaxBase(decimal grossAmount, decimal? charitySpent)
        {
            decimal taxBase = grossAmount - taxFreeAmount;

            if (charitySpent == null || charitySpent == 0)
            {
                return taxBase;
            }
            
            decimal charitySpentPercentage = (decimal)charitySpent / grossAmount;

            if (charitySpentPercentage > charitySpentMaxPercentage)
            {
                return taxBase - (grossAmount * charitySpentMaxPercentage);
            }

            return taxBase - (decimal)charitySpent;
        }

    }
}
