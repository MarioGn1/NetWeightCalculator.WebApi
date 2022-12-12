using System;

namespace NetWeightCalculator.Services.Models
{
    public class IdentityCacheKey : IEquatable<IdentityCacheKey>
    {
        private string ssn;
        private string country;
        private string fullName;

        private IdentityCacheKey(string ssn, string country)
        {
            this.SSN = ssn;
            this.Country = country;
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

        public string FullName { 
            get => fullName;
            set 
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Full name cannot be null or empty.");
                else
                    this.fullName = value;
            }
        }

        public static IdentityCacheKey From(PayerDto payer)
            => new(payer.SSN, payer.Country);

        public bool Equals(IdentityCacheKey other)
        {
            if (other == null)
                return false;

            if (this.ssn != other.ssn)
                return false;

            if (this.country != other.country)
                return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            IdentityCacheKey identityObj = obj as IdentityCacheKey;
            if (identityObj == null)
                return false;
            else
                return Equals(identityObj);
        }

        public override int GetHashCode()
        {
            return this.SSN.GetHashCode();
        }

        public static bool operator ==(IdentityCacheKey person1, IdentityCacheKey person2)
        {
            if (((object)person1) == null || ((object)person2) == null)
                return Equals(person1, person2);

            return person1.Equals(person2);
        }

        public static bool operator !=(IdentityCacheKey person1, IdentityCacheKey person2)
        {
            if (((object)person1) == null || ((object)person2) == null)
                return !Equals(person1, person2);

            return !(person1.Equals(person2));
        }
    }
}
