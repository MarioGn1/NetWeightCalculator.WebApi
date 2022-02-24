using NetWeightCalculator.DTOs;

namespace NetWeightCalculator.Services.CalculatorServices
{
    public interface ICalculatorService
    {
        public TaxesResponseModel Calculate(PayerRequestModel model);
    }
}
