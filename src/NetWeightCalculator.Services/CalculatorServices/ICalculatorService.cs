using NetWeightCalculator.Services.Models;

namespace NetWeightCalculator.Services.CalculatorServices
{
    public interface ICalculatorService
    {
        public TaxesDto Calculate(PayerDto model);
    }
}
