namespace NetWeightCalculator.DTOs.Models
{
    public class JurisdictionTaxModel
    {
        protected JurisdictionTaxModel(
            decimal taxFreeAmount,
            decimal incomeTax,
            decimal socialContributionsTax,
            decimal socialContributionsUperLimit,
            decimal charitySpentMaxPercentage)
        {
            TaxFreeAmount = taxFreeAmount;
            IncomeTax = incomeTax;
            SocialContributionsTax = socialContributionsTax;
            SocialContributionsUperLimit = socialContributionsUperLimit;
            CharitySpentMaxPercentage = charitySpentMaxPercentage;
        }

        public decimal TaxFreeAmount { get; private set; }
        public decimal IncomeTax { get; private set; }
        public decimal SocialContributionsTax { get; private set; }
        public decimal SocialContributionsUperLimit { get; private set; }
        public decimal CharitySpentMaxPercentage { get; private set; }

        public static JurisdictionTaxModel From(
            decimal taxFreeAmount,
            decimal incomeTax,
            decimal socialContributionsTax,
            decimal socialContributionsUperLimit,
            decimal charitySpentMaxPercentage)
            => new(taxFreeAmount, incomeTax, socialContributionsTax, socialContributionsUperLimit, charitySpentMaxPercentage);
    }
}
