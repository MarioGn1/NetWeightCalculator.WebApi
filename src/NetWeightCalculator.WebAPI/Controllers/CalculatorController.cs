using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NetWeightCalculator.DTOs;
using NetWeightCalculator.Services.CalculatorServices;
using System.Linq;
using System.Globalization;
using System.Threading;

namespace NetWeightCalculator.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculatorService calculatorService;
        private readonly IStringLocalizer<CalculatorController> localizer;

        public CalculatorController(ICalculatorService calculatorService,
            IStringLocalizer<CalculatorController> localizer)
        {
            this.calculatorService = calculatorService;
            this.localizer = localizer;
        }

        [HttpPost]
        [Route("calculate-taxes")]
        public ActionResult<TaxesResponseModel> Calculate(PayerRequestModel payerModel)
        {
            try
            {
                var taxModel = calculatorService.GetTaxModel(localizer);
                var responceModel = calculatorService.Calculate(payerModel, taxModel);
                return responceModel;
            }
            catch (System.Exception)
            {
                return BadRequest("Server failed to calculate the requested data.");
            }       
        }
    }
}
