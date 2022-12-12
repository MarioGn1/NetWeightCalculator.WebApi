﻿namespace NetWeightCalculator.Services
{
    internal static class ServicesConstants
    {
        internal class BadOperationMessages
        {
            internal const string TAX_RATES_FAILED_TO_LOAD = "Failed to establish the choosen jurisdiction tax rates.";
            internal const string CALCULATION_FAILED = "Failed to caculate requested data.";
            internal const string INVALID_PERSONAL_DATA_INPUT = "Wrong SSN or invalid payer personal data input.";
        }
    }
}
