using Microsoft.Extensions.Localization;
using NetWeightCalculator.DTOs.Models;

namespace NetWeightCalculator.Services.CalculatorServices
{
    public interface ICalculatorService
    {
        public TaxesResponseModel Calculate(PayerRequestModel model, JurisdictionTaxModel taxModel);
        public JurisdictionTaxModel GetTaxModel(IStringLocalizer localizer);
    }
}
