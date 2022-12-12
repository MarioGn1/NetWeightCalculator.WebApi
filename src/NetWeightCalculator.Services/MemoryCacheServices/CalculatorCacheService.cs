using Microsoft.Extensions.Caching.Memory;
using NetWeightCalculator.Services.Models;
using System;

namespace NetWeightCalculator.Services.MemoryCacheServices
{
    public class CalculatorCacheService : ICalculatorCacheService
    {
        private readonly IMemoryCache cache;

        public CalculatorCacheService(IMemoryCache cache)
        {
            this.cache = cache;
        }

        public bool IsExistentPayer(IdentityCacheKey cacheKey)
            => this.cache.TryGetValue(cacheKey, out var _);

        public void SetPayerIdentity(IdentityCacheKey cacheKey)
            => this.cache.Set<object>(cacheKey, null);

        public TaxesDto GetTaxes(TaxesCalculationCacheKey cacheKey)
            => this.cache.TryGetValue(cacheKey, out TaxesDto payerCacheModel)
                ? payerCacheModel
                : null;

        public void SetTaxes(TaxesCalculationCacheKey cacheKey, TaxesDto payerCacheModel)
        {
            var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

            this.cache.Set(cacheKey, payerCacheModel, cacheOptions);
        }
    }
}
