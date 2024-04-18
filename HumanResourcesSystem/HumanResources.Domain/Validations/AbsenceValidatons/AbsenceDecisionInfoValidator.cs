using FluentValidation;
using HumanResources.Domain.AbsenceModelDto;

namespace HumanResources.Domain.Validations.AbsenceValidatons
{
    public class AbsenceDecisionInfoValidator : AbstractValidator<AbsenceDecisionInfoDto>
    {
        public AbsenceDecisionInfoValidator()
        {
            RuleFor(pr => pr.AbsenceId)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(6);
                
        }
    }
}
