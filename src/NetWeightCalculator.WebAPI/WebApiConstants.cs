namespace NetWeightCalculator.WebAPI
{
    internal static class WebApiConstants
    {
        internal class BadRequestMessages
        {
            public const string TAX_RATES_FAILED_TO_LOAD = "Failed to establish the choosen jurisdiction tax rates.";
            public const string CALCULATION_FAILED = "Failed to caculate requested data.";
        }
    }
}
