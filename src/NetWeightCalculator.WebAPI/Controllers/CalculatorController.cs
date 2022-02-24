using Microsoft.AspNetCore.Mvc;
using NetWeightCalculator.DTOs;
using NetWeightCalculator.Services.CalculatorServices;
using System.Linq;

namespace NetWeightCalculator.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculatorService calculatorService;

        public CalculatorController(ICalculatorService calculatorService)
        {
            this.calculatorService = calculatorService;
        }

        [HttpPost]
        [Route("calculate-taxes")]
        public TaxesResponseModel Calculate(PayerRequestModel model)
        {            
            return calculatorService.Calculate(model);
        }
    }
}
