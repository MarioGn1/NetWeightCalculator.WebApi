using Microsoft.AspNetCore.Mvc;
using NetWeightCalculator.DTOs;

namespace NetWeightCalculator.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        [HttpPost]
        [Route("calculate-taxes")]
        public TaxesResponseModel Calculate(PayerRequestModel model)
        {
            return new TaxesResponseModel()
            {
                GrossIncome = 3700,
                CharitySpent = 500,
                IncomeTax = 370,
                SocialTax = 500,
                TotalTax = 870,
                NetIncome = 2830
            };
        }
    }
}
