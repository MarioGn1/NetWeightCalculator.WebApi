using Microsoft.AspNetCore.Mvc;
using NetWeightCalculator.Services.CalculatorServices;
using NetWeightCalculator.WebAPI.Models;
using NetWeightCalculator.Services.Models;

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
        [Route("calculate")]
        public ActionResult<CalculateTaxesResponseModel> Calculate(CalculateTaxesRequestModel request)
        {
            var result = calculatorService.Calculate(PayerDto.From(
                request.Country,
                request.SSN,
                request.FullName,
                request.DateOfBirth,
                request.GrossIncome,
                request.CharitySpent!.Value));

            return CalculateTaxesResponseModel.From(result);
        }


    }
}
