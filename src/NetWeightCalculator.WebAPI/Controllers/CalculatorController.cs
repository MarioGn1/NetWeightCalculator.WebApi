using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NetWeightCalculator.DTOs;
using NetWeightCalculator.Services.CalculatorServices;
using System;
using Microsoft.Extensions.Caching.Memory;

using static NetWeightCalculator.WebAPI.WebApiConstants.Cache;

namespace NetWeightCalculator.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculatorService calculatorService;
        private readonly IStringLocalizer<CalculatorController> localizer;
        private readonly IMemoryCache cache;
        private JurisdictionTaxModel taxModel;
        private TaxesResponseModel responceModel;

        public CalculatorController(
                ICalculatorService calculatorService,
                IStringLocalizer<CalculatorController> localizer,
                IMemoryCache cache)
        {
            this.calculatorService = calculatorService;
            this.localizer = localizer;
            this.cache = cache;
        }

        [HttpPost]
        [Route("calculate-taxes")]
        public ActionResult<TaxesResponseModel> Calculate(PayerRequestModel payerModel)
        {
            try
            {
                taxModel = calculatorService.GetTaxModel(localizer);
            }
            catch (Exception)
            {
                return BadRequest("Failed to establish the choosen jurisdiction tax rates.");
            }
            try
            {
                responceModel = calculatorService.Calculate(payerModel, taxModel);
            }
            catch (Exception)
            {
                return BadRequest("Failed to caculate requested data.");
            }

            CachingPayer(payerModel);

            return responceModel;
        }

        private void CachingPayer(PayerRequestModel payerModel)
        {
            var payerModelInCache = this.cache.Get<PayerRequestModel>(PayerCacheKey);

            if (payerModelInCache == null)
            {
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(PayerCacheKey, payerModel, cacheOptions);
            }
        }
    }
}
