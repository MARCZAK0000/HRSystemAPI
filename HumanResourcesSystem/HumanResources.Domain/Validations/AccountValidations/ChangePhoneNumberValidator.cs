using FluentValidation;
using HumanResources.Domain.ModelDtos;

namespace HumanResources.Domain.Validations.AccountValidations
{
    public class ChangePhoneNumberValidator : AbstractValidator<ChangePhoneNumberDto>
    {
        private readonly string nubmers = "0123456789+";
        public ChangePhoneNumberValidator()
        {
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


            RuleFor(pr => pr.ConfirmPhoneNumber)
                .Equal(pr => pr.PhoneNumber);
        }
    }
}
