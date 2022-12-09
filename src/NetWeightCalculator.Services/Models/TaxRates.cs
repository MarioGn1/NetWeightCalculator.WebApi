using System.Collections.Generic;

namespace NetWeightCalculator.Services.Models
{
    public sealed record TaxRates(IReadOnlyCollection<Jurisdiction> Jurisdictions);

    public sealed record Jurisdiction(
        string Country,
        string Currency,
        decimal CharitySpentMaxPercentage,
        decimal IncomeTax,
        decimal SocialContributionsTax,
        decimal SocialContributionsUperLimit,
        decimal TaxFreeAmount);
}
