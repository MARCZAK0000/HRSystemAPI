using FluentValidation;
using HumanResources.Domain.ModelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.Validations
{
    public class LoginUserValidator:AbstractValidator<LoginAccountAsyncDto>
    {
        public LoginUserValidator()
        {
            RuleFor(pr => pr.Email)
                 .EmailAddress()
                 .Length(5, 150);

            RuleFor(pr => pr.Password)
                .NotNull()
                .Length(8, 18);
        }
    }
}
