using NetWeightCalculator.DTOs.CustomAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace NetWeightCalculator.DTOs
{
    public class PayerRequestModel
    {
        public int SSN { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]{2,20}(?:\s[a-zA-Z]{2,20}){1,6}$", ErrorMessage = "Full name must be at least two words and must contains only letters and white spaces. Each name must be at least two characters long.")]
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        [PrecisionAndScale(12, 2, ErrorMessage = "Gross income must be positive number and must not exceed 9999999999.99")]
        public decimal GrossIncome { get; set; }
        [PrecisionAndScale(12, 2, ErrorMessage = "Charity spent must be positive number and must not exceed 9999999999.99")]
        public decimal? CharitySpent { get; set; }
    }
}
