using FluentValidation;
using HumanResources.Domain.AdditionalHoursDto;

namespace HumanResources.Domain.Validations.AdditionalHours
{
    public class ShowAllAdditionalHoursValidator:AbstractValidator<ShowAllAdditionalHoursDto>
    {
        public ShowAllAdditionalHoursValidator()
        {
            RuleFor(pr=>pr.UserCode).NotEmpty().WithMessage("Invalid UserCode");

            RuleFor(pr => pr.PageNumber).GreaterThanOrEqualTo(0).WithMessage("Invalid value");

            RuleFor(pr => pr.PageSize).GreaterThanOrEqualTo(1).WithMessage("Invalid value");
        }
    }
}
