using FluentValidation;
using HumanResources.Domain.ModelDtos;

namespace HumanResources.Domain.Validations.AccountValidations
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordAsyncDto>
    {
        public ResetPasswordValidator()
        {

            RuleFor(pr => pr.Email)
                 .EmailAddress()
                 .Length(5, 150);

            RuleFor(pr => pr.Password)
                .NotNull()
                .Length(8, 18);

            RuleFor(pr => pr.ConfirmPassword)
                .Equal(pr => pr.Password)
                .WithMessage("Confirm password and password have to be the same"); ;
        }
    }
}
