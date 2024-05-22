using FluentValidation;
using HumanResources.Domain.FinancialModelDto;

namespace HumanResources.Domain.Validations.FinancialValidations
{
    public class SetUserPaymentRateValidator : AbstractValidator<SetUserPaymentRateDto>
    {
        public SetUserPaymentRateValidator()
        {
            RuleFor(pr => pr.RatePLN)
                .GreaterThan(20);

            RuleFor(pr => pr.UserCode)
                .NotEmpty();
        }
    }
}
