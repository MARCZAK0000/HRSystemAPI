using FluentValidation;
using HumanResources.Domain.FinancialModelDto;

namespace HumanResources.Domain.Validations.FinancialValidations
{
    public class GetExchangeRateValidator:AbstractValidator<GetExchangeRateAsyncDto>
    {
        public GetExchangeRateValidator()
        {
            RuleFor(pr => pr.CurrencyCode).Length(3);
        }
    }
}
