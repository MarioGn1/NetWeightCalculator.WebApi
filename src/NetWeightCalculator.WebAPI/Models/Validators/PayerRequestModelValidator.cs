using FluentValidation;
using static NetWeightCalculator.WebAPI.WebApiConstants.ErrorMessages;
using static NetWeightCalculator.WebAPI.WebApiConstants.DefaultValues;
using static NetWeightCalculator.WebAPI.WebApiConstants.RegularExpressionPaterns;

namespace NetWeightCalculator.WebAPI.Models.Validators
{
    public class PayerRequestModelValidator : AbstractValidator<CalculateTaxesRequestModel>
    {
        public PayerRequestModelValidator()
        {
            RuleFor(m => m.SSN)
                .NotEmpty()
                .Matches(SSN_PATERN)
                .WithMessage(SSN_VALIDATION_ERROR);
            RuleFor(m => m.FullName)
                .NotEmpty()
                .Matches(FULL_NAME_PATERN)
                .WithMessage(FULL_NAME_VALIDATION_ERROR);
            RuleFor(m => m.GrossIncome)
                .NotEmpty()
                .GreaterThan(0)
                .PrecisionScale(DEFAULT_PRECISION, DEFAULT_SCALE, IGNORE_TRAILING_ZEROES)
                .WithMessage(GROSS_INCOME_VALIDATION_ERROR);
            RuleFor(m => m.CharitySpent)
                .GreaterThan(-1)
                .PrecisionScale(DEFAULT_PRECISION, DEFAULT_SCALE, IGNORE_TRAILING_ZEROES)
                .WithMessage(CHARITY_SPENT_VALIDATION_ERROR);
        }
    }
}
