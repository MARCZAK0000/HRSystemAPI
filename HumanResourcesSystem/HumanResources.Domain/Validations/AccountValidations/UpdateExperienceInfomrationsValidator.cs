using FluentValidation;
using HumanResources.Domain.ModelDtos;

namespace HumanResources.Domain.Validations.AccountValidations
{
    public class UpdateExperienceInfomrationsValidator:AbstractValidator<UpdateExperienceInfomrationsDto>
    {
        public UpdateExperienceInfomrationsValidator()
        {
            RuleFor(pr => pr.Level)
                .NotEmpty()
                .IsInEnum();

            RuleFor(pr => pr.YearsOfExperience)
                .GreaterThan(0)
                .LessThan(100)
                .NotEmpty();
                
        }
    }
}
