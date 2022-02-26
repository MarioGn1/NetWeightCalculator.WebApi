using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NetWeightCalculator.DTOs.Models;
using NetWeightCalculator.Services.CalculatorServices;
using System;
using Microsoft.Extensions.Caching.Memory;

using static NetWeightCalculator.WebAPI.WebApiConstants.BadOperationMessages;

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
                CachingPayerData(payerModel);
                taxModel = calculatorService.GetTaxModel(localizer);
                responceModel = calculatorService.Calculate(payerModel, taxModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }         

            return responceModel;
        }

        private void CachingPayerData(PayerRequestModel payerModel)
        {
            var payerCacheModel = this.cache.Get<PayerRequestModel>(payerModel.SSN);

            if (payerCacheModel == null)
            {
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(payerModel.SSN, payerModel, cacheOptions);
            }
            else if (!payerModel.Equals(payerCacheModel))
            {
                throw new InvalidOperationException(INVALID_PERSONAL_DATA_INPUT);
            }
        }
    }
}
