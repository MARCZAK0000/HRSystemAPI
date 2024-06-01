using FluentValidation;
using HumanResources.Domain.AdditionalHoursDto;

namespace HumanResources.Domain.Validations.AdditionalHours
{
    public class ShowAdditionalHoursValidator:AbstractValidator<ShowAdditionalHoursDto>
    {
        public ShowAdditionalHoursValidator()
        {
            RuleFor(pr => pr.AdditionalHoursID)
                .GreaterThanOrEqualTo(0);

            RuleFor(pr => pr.UserCode)
                .NotEmpty().WithMessage("Invalid UserCode");

        }
    }
}
