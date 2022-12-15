namespace NetWeightCalculator.Services.Models
{
    public sealed class TaxRates
    {
        public Jurisdiction[] Jurisdictions { get; set; }
    }

    public sealed class Jurisdiction
    {
        public string Country { get; set; }
        public string Currency { get; set; }
        public decimal CharitySpentMaxPercentage { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal SocialContributionsTax { get; set; }
        public decimal SocialContributionsUperLimit { get; set; }
        public decimal TaxFreeAmount { get; set; }
    }
}
