namespace NetWeightCalculator.DTOs
{
    internal static class ModelConstants
    {
        internal class ErrorMessages
        {
            public const string FULL_NAME_VALIDATION_ERROR = "Full name must be at least two words and must contains only letters and white spaces. Each name must be at least two characters long.";
            public const string GROSS_INCOME_VALIDATION_ERROR = "Gross income must be positive number, must follow 9999999999.99 format and must not exceed it.";
            public const string CHARITY_SPENT_VALIDATION_ERROR = "Charity spent must be positive number, must follow 9999999999.99 format and must not exceed it.";
            public const string SSN_VALIDATION_ERROR = "SSN must be between 5 and 10 digits long.";
        }        

        internal class RegularExpressionPaterns
        {
            public const string FULL_NAME_PATERN = @"^[a-zA-Z]{2,20}(?:\s[a-zA-Z]{2,20}){1,6}$";
            public const string SSN_PATERN = @"^\d{5,10}$";
        }
    }
}
