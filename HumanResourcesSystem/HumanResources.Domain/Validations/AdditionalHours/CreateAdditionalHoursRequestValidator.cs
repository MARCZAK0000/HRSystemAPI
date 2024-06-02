using FluentValidation;
using HumanResources.Domain.AdditionalHoursDto;

namespace HumanResources.Domain.Validations.AdditionalHours
{
    public class CreateAdditionalHoursRequestValidator:AbstractValidator<CreateAdditionalHoursRequestDto>
    {
        public CreateAdditionalHoursRequestValidator()
        {
            RuleFor(pr => pr.Title)
                .NotEmpty().WithMessage("Is Empty")
                .MaximumLength(100).WithMessage("Too long");

            RuleFor(pr => pr.UserCode)
                .NotEmpty().WithMessage("Invalid UserCode");

            RuleFor(pr => pr.StartTime)
                .LessThan(pr => pr.EndTime).WithMessage("Invalid Date")
                .GreaterThan(DateTime.Now).WithMessage("Invalid Date1");


            RuleFor(pr => pr.EndTime)
                .GreaterThan(pr => pr.StartTime).WithMessage("Invalid Date")
                .GreaterThan(DateTime.Now).WithMessage("Invalid Date1");
        }
    }
}
