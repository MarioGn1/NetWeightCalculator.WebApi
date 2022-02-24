using System;

namespace NetWeightCalculator.DTOs
{
    public class PayerRequestModel
    {
        public int SSN { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal GrossIncome { get; set; }
        public decimal? CharitySpent { get; set; }
    }
}
