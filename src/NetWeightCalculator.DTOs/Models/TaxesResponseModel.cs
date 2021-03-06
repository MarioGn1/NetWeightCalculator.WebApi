namespace NetWeightCalculator.DTOs.Models
{
    public class TaxesResponseModel
    {
        public decimal GrossIncome { get; set; }
        public decimal? CharitySpent { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal SocialTax { get; set; }
        public decimal TotalTax { get; set; }
        public decimal NetIncome { get; set; }
    }
}
