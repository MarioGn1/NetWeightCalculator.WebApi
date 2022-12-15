namespace NetWeightCalculator.Services.Models
{
    public sealed record TaxesDto(
        decimal GrossIncome,
        decimal? CharitySpent,
        decimal NetIncome,
        decimal IncomeTax = 0,
        decimal SocialTax = 0,
        decimal TotalTax = 0)
    {
        public static TaxesDto NoCharges(decimal grossIncome, decimal? charitySpent, decimal netIncome)
            => new(grossIncome, charitySpent, netIncome);

        public static TaxesDto ApplyCharges(
            decimal grossIncome,
            decimal? charitySpent,
            decimal netIncome,
            decimal incomeTax,
            decimal socialTax,
            decimal totalTax)
            => new(grossIncome, charitySpent, netIncome, incomeTax, socialTax, totalTax);
    }
}
