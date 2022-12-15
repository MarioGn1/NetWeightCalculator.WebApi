using Microsoft.Extensions.Caching.Memory;
using NetWeightCalculator.Services.Models;
using System;

namespace NetWeightCalculator.Services.MemoryCacheServices
{
    public class CalculatorCacheService : ICalculatorCacheService
    {
        private readonly IMemoryCache _cache;

        public CalculatorCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public bool IsExistentPayer(IdentityCacheKey cacheKey)
            => _cache.TryGetValue(cacheKey, out var _);

        public void SetPayerIdentity(IdentityCacheKey cacheKey)
            => _cache.Set<object>(cacheKey, null);

        public TaxesDto GetTaxes(TaxesCalculationCacheKey cacheKey)
            => _cache.TryGetValue(cacheKey, out TaxesDto payerCacheModel)
                ? payerCacheModel
                : null;

        public void SetTaxes(TaxesCalculationCacheKey cacheKey, TaxesDto payerCacheModel)
        {
            var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

            _cache.Set(cacheKey, payerCacheModel, cacheOptions);
        }
    }
}
