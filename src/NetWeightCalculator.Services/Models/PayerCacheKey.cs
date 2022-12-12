using System;

namespace NetWeightCalculator.Services.Models
{
    public sealed class TaxesCalculationCacheKey : IEquatable<TaxesCalculationCacheKey>
    {
        private string country;
        private string ssn;
        private decimal grossIncome;
        private decimal charitySpent;

        private TaxesCalculationCacheKey(string country, string ssn, decimal grossIncome, decimal charitySpent)
        {
            this.Country = country;
            this.ssn = ssn;
            this.GrossIncome = grossIncome;
            this.CharitySpent = charitySpent;
        }

        public decimal GrossIncome
        {
            get => this.grossIncome;
            set
            {
                if (value < 0 || value > decimal.MaxValue)
                    throw new ArgumentException("Invalid Gross Income Value.");
                else
                    this.grossIncome = value;
            }
        }
        public decimal CharitySpent
        {
            get => this.charitySpent;
            set
            {
                if (value < 0 || value > decimal.MaxValue)
                    throw new ArgumentException("Invalid Charity Spent Value.");
                else
                    this.charitySpent = value;
            }
        }
        public string Country
        {
            get => this.country;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Country cannot be null or empty.");
                else
                    this.country = value;
            }
        }
        public string SSN
        {
            get => this.ssn;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Ssn cannot be null or empty.");
                else
                    this.ssn = value;
            }
        }

        public static TaxesCalculationCacheKey From(PayerDto payer)
            => new(payer.Country, payer.SSN, payer.GrossIncome, payer.CharitySpent);

        public bool Equals(TaxesCalculationCacheKey other)
        {
            if (other == null)
                return false;

            if (this.country != other.country)
                return false;

            if (this.ssn != other.ssn)
                return false;

            if (this.grossIncome != other.grossIncome)
                return false;

            if (this.charitySpent != other.charitySpent)
                return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            TaxesCalculationCacheKey personObj = obj as TaxesCalculationCacheKey;
            if (personObj == null)
                return false;
            else
                return Equals(personObj);
        }

        public override int GetHashCode()
        {
            return this.Country.GetHashCode();
        }

        public static bool operator ==(TaxesCalculationCacheKey person1, TaxesCalculationCacheKey person2)
        {
            if (((object)person1) == null || ((object)person2) == null)
                return Equals(person1, person2);

            return person1.Equals(person2);
        }

        public static bool operator !=(TaxesCalculationCacheKey person1, TaxesCalculationCacheKey person2)
        {
            if (((object)person1) == null || ((object)person2) == null)
                return !Equals(person1, person2);

            return !(person1.Equals(person2));
        }
    }
}
