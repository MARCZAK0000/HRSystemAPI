using FluentValidation;
using HumanResources.Domain.ModelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.Validations.AccountValidations
{
    public class UpdateUserInformationsValidator : AbstractValidator<UpdateAccountInformationsDto>
    {
        public UpdateUserInformationsValidator()
        {
            RuleFor(pr => pr.FirstName)
              .NotNull()
              .Length(2, 50);

            RuleFor(pr => pr.FirstName)
                .NotNull()
                .Length(2, 50);


            RuleFor(pr => pr.EducationLevel)
                .NotNull()
                .IsInEnum();

            RuleFor(pr => pr.YearsOfExperiences)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100);


        }
    }
}
