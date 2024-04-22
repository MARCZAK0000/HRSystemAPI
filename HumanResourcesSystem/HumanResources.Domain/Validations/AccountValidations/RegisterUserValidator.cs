using FluentValidation;
using HumanResources.Domain.ModelDtos;

namespace HumanResources.Domain.Validations.AccountValidations
{
    public class RegisterUserValidator : AbstractValidator<RegisterAccountAsyncDto>
    {
        private readonly string nubmers = "0123456789+";
        public RegisterUserValidator()
        {
            RuleFor(pr => pr.Email)
                .EmailAddress()
                .Length(5, 150);

            RuleFor(pr => pr.Password)
                .NotNull()
                .Length(8, 18);

            RuleFor(pr => pr.ConfirmPassword)
                .NotNull()
                .Length(8, 18)
                .Equal(pr => pr.Password).WithMessage("Password and Confrim Password must be equal");

            RuleFor(pr => pr.PhoneNumber)
                .NotNull()
                .Length(9, 10)
                .Custom(
                (value, context) =>
                {
                    var isContainsWrongDigits = false;
                    foreach (var item in value)
                    {
                        if (!nubmers.ToLower().Contains(item))
                        {
                            isContainsWrongDigits = true;
                            break;
                        }
                    }
                    if (isContainsWrongDigits)
                    {
                        context.AddFailure("Phone number has wrong digits");
                    }
                });
        }
    }
}
