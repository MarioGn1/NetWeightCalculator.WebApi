using System;
using static NetWeightCalculator.WebAPI.WebApiConstants.DefaultValues;

namespace NetWeightCalculator.WebAPI.Models
{
    public class CalculateTaxesRequestModel
    {
        public string Country { get; set; } = DEFAULT_COUNTRY;
        public string SSN { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
        public decimal GrossIncome { get; set; }
        public decimal? CharitySpent { get; set; } = DEFAULT_CHARITY_SPENT;
    }
}
