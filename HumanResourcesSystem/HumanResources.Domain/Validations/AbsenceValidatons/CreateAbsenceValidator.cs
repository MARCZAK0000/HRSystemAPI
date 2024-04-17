using FluentValidation;
using HumanResources.Domain.AbsenceModelDto;

namespace HumanResources.Domain.Validations.AttendanceValidatons
{
    public class CreateAbsenceValidator : AbstractValidator<CreateAbsenceDto>
    {
        public CreateAbsenceValidator()
        {
            RuleFor(pr => pr.StartTime)
                .NotEmpty()
                .LessThan(pr=>pr.EndTime)
                .GreaterThanOrEqualTo(pr=>pr.CreatedTime.AddDays(1));

            RuleFor(pr => pr.EndTime)
                .NotEmpty();

            RuleFor(pr => pr.AbsenceTypeId)
                .GreaterThan(0)
                .LessThanOrEqualTo(6);

            RuleFor(pr => pr.Name)
                .MinimumLength(4)
                .MaximumLength(25);
            
        }
    }
}
