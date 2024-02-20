using FluentValidation;
using HumanResources.Domain.ModelDtos;

namespace HumanResources.Domain.Validations
{
    public class ChangePasswordValidator:AbstractValidator<ChangePasswordAsyncDto>
    {
        public ChangePasswordValidator()
        {
            RuleFor(pr=>pr.Password)
                .NotNull()
                .Length(8,18);

            RuleFor(pr => pr.ConfirmPassword)
                .Equal(pr => pr.Password)
                .WithMessage("Confirm password and password have to be the same"); ;

            RuleFor(pr=>pr.NewPassword)
                .NotNull()
                .Length(8, 18);

            RuleFor(pr=>pr.ConfirmNewPassword)
                .Equal(pr => pr.NewPassword)
                .WithMessage("Confirm new password and new password have to be the same");
        }
    }
}
