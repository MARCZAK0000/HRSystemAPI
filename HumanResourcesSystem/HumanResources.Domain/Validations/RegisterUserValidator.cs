using FluentValidation;
using HumanResources.Domain.ModelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.Validations
{
    public class RegisterUserValidator:AbstractValidator<RegisterUserAsyncDto>
    {
        public RegisterUserValidator()
        {
            RuleFor(pr => pr.Email)
                .EmailAddress()
                .Length(5, 150);

            RuleFor(pr => pr.Password)
                .NotNull()
                .Length(8, 18);

            RuleFor(pr => pr.ConfirmPassword)
                .NotNull()
                .Length(8,18)
                .Equal(pr => pr.Password).WithMessage("Password and Confrim Password must be equal");

            RuleFor(pr => pr.PhoneNumber)
                .NotNull()
                .Length(9, 10);

            RuleFor(pr => pr.FirstName)
                .NotNull()
                .Length(2, 50);
            
            RuleFor(pr => pr.FirstName)
                .NotNull()
                .Length(2, 50);

        }
    }
}
