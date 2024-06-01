using FluentValidation;
using HumanResources.Domain.AdditionalHoursDto;

namespace HumanResources.Domain.Validations.AdditionalHours
{
    public class DeleteAdditionalHoursRequestValidator:AbstractValidator<DeleteAdditionalHoursRequestDto>
    {
        public DeleteAdditionalHoursRequestValidator()
        {
            RuleFor(pr => pr.AdditionalHoursID)
                .GreaterThanOrEqualTo(0);

            RuleFor(pr => pr.UserCode)
                .NotEmpty().WithMessage("Invalid UserCode");

        }
    }
}
