using NetWeightCalculator.Services.Models;

namespace NetWeightCalculator.Services.MemoryCacheServices
{
    public interface ICalculatorCacheService
    {
        bool IsExistentPayer(IdentityCacheKey cacheKey);
        void SetPayerIdentity(IdentityCacheKey cacheKey);
        TaxesDto GetTaxes(TaxesCalculationCacheKey cacheKey);
        void SetTaxes(TaxesCalculationCacheKey cacheKey, TaxesDto payerCacheModel);
    }
}
