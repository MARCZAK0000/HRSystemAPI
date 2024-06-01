using FluentValidation;
using HumanResources.Domain.AdditionalHoursDto;

namespace HumanResources.Domain.Validations.AdditionalHours
{
    public class ShowAdditionalHoursDateValidator:AbstractValidator<ShowAdditionalHoursDateDto>
    {
        public ShowAdditionalHoursDateValidator()
        {
            RuleFor(pr => pr.UserCode)
                .Length(13).WithMessage("Invalid UserCode");

            RuleFor(pr => pr.PageNumber)
                .GreaterThanOrEqualTo(0).WithMessage("Invalid value");

            RuleFor(pr => pr.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("Invalid value");

            RuleFor(pr => pr.From)
                .LessThan(pr => pr.To).WithMessage("Invalid Date");

            RuleFor(pr => pr.To)
                .GreaterThan(pr => pr.To).WithMessage("Invalid Date");
        }
    }
}
