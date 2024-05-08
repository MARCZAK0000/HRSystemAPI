using FluentValidation;
using HumanResources.Domain.ModelDtos;

namespace HumanResources.Domain.Validations.AccountValidations
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenDto>
    {
        public RefreshTokenValidator()
        {
            RuleFor(pr => pr.RefreshToken).NotEmpty();
        }
    }
}
