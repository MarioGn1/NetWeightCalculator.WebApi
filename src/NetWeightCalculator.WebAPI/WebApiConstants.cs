namespace NetWeightCalculator.WebAPI
{
    internal static class WebApiConstants
    {
        internal class DefaultValues
        {
            internal const string DEFAULT_COUNTRY = "Imaginaria";
            internal const int DEFAULT_PRECISION = 12;
            internal const int DEFAULT_SCALE = 2;
            internal const decimal DEFAULT_CHARITY_SPENT = 0;
            internal const bool IGNORE_TRAILING_ZEROES = true;
        }

        internal class ErrorMessages
        {
            internal const string FULL_NAME_VALIDATION_ERROR = "Full name must be at least two words and must contains only letters and white spaces. Each name must be at least two characters long.";
            internal const string GROSS_INCOME_VALIDATION_ERROR = "Gross income must be positive number, must follow 9999999999.99 format and must not exceed it.";
            internal const string CHARITY_SPENT_VALIDATION_ERROR = "Charity spent must be positive number, must follow 9999999999.99 format and must not exceed it.";
            internal const string SSN_VALIDATION_ERROR = "SSN must be between 5 and 10 digits long.";
        }

        internal class RegularExpressionPaterns
        {
            internal const string FULL_NAME_PATERN = @"^[a-zA-Z]{2,20}(?:\s[a-zA-Z]{2,20}){1,6}$";
            internal const string SSN_PATERN = @"^\d{5,10}$";
        } 
    } 
}
