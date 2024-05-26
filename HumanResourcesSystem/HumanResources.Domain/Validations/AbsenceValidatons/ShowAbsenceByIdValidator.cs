using FluentValidation;
using HumanResources.Domain.AbsenceModelDto;

namespace HumanResources.Domain.Validations.AbsenceValidatons
{
    public class ShowAbsenceByIdValidator:AbstractValidator<ShowAbsenceByIdDto>
    {
        public ShowAbsenceByIdValidator()
        {
            RuleFor(pr=>pr.AbsenceId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(pr => pr.UserCode)
                .Length(12);
        }
    }
}
