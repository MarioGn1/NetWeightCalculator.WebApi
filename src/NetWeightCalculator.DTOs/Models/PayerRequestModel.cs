using NetWeightCalculator.DTOs.CustomAttributes;
using System;
using System.ComponentModel.DataAnnotations;

using static NetWeightCalculator.DTOs.ModelConstants.ErrorMessages;
using static NetWeightCalculator.DTOs.ModelConstants.RegularExpressionPaterns;

namespace NetWeightCalculator.DTOs.Models
{
    public class PayerRequestModel
    {
        public int SSN { get; set; }
        [Required]
        [RegularExpression(FULL_NAME_PATERN, ErrorMessage = FULL_NAME_VALIDATION_ERROR)]
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        [PrecisionAndScale(12, 2, ErrorMessage = GROSS_INCOME_VALIDATION_ERROR)]
        public decimal GrossIncome { get; set; }
        [PrecisionAndScale(12, 2, ErrorMessage = CHARITY_SPENT_VALIDATION_ERROR)]
        public decimal? CharitySpent { get; set; }
    }
}
