using System;

namespace NetWeightCalculator.Services.Models
{
    public class PayerDto
    {
        protected PayerDto(
            string country,
            string sSN,
            string fullName, 
            DateTime dateOfBirth, 
            decimal grossIncome,
            decimal? charitySpent)
        {
            Country = country;
            SSN = sSN;
            FullName = fullName;
            DateOfBirth = dateOfBirth;
            GrossIncome = grossIncome;
            CharitySpent = charitySpent;
        }

        public string Country { get; private set; }
        public string SSN { get; private set; }
        public string FullName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public decimal GrossIncome { get; private set; }
        public decimal? CharitySpent { get; private set; }

        public static PayerDto From(
            string country,
            string sSN,
            string fullName,
            DateTime dateOfBirth,
            decimal grossIncome,
            decimal? charitySpent)
            => new(country, sSN, fullName, dateOfBirth, grossIncome, charitySpent);
    }
}
