using NetWeightCalculator.Services.Models;

namespace NetWeightCalculator.WebAPI.Models
{
    public sealed record CalculateTaxesResponseModel(
        decimal GrossIncome,
        decimal? CharitySpent,
        decimal IncomeTax,
        decimal SocialTax,
        decimal TotalTax,
        decimal NetIncome)
    {
        public static CalculateTaxesResponseModel From(TaxesDto taxesCalculation)
            => new(
                taxesCalculation.GrossIncome,
                taxesCalculation.CharitySpent,
                taxesCalculation.IncomeTax,
                taxesCalculation.SocialTax,
                taxesCalculation.TotalTax,
                taxesCalculation.NetIncome);
    }
}
