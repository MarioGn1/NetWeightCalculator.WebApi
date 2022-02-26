using NetWeightCalculator.DTOs.CustomAttributes;
using System;
using System.ComponentModel.DataAnnotations;

using static NetWeightCalculator.DTOs.ModelConstants.ErrorMessages;
using static NetWeightCalculator.DTOs.ModelConstants.RegularExpressionPaterns;

namespace NetWeightCalculator.DTOs.Models
{
    public class PayerRequestModel
    {
        [RegularExpression(SSN_PATERN, ErrorMessage = SSN_VALIDATION_ERROR)]
        public int SSN { get; set; }
        [Required]
        [RegularExpression(FULL_NAME_PATERN, ErrorMessage = FULL_NAME_VALIDATION_ERROR)]
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        [PrecisionAndScale(12, 2, ErrorMessage = GROSS_INCOME_VALIDATION_ERROR)]
        public decimal GrossIncome { get; set; }
        [PrecisionAndScale(12, 2, ErrorMessage = CHARITY_SPENT_VALIDATION_ERROR)]
        public decimal? CharitySpent { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is PayerRequestModel)
            {
                PayerRequestModel m = obj as PayerRequestModel;
                if (m.SSN != this.SSN) return false;
                if (m.FullName != this.FullName) return false;
                if (m.DateOfBirth != this.DateOfBirth) return false;

                return true;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
