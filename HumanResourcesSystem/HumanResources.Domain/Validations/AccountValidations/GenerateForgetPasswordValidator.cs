﻿using FluentValidation;
using HumanResources.Domain.ModelDtos;
using Org.BouncyCastle.Crypto.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.Validations.AccountValidations
{
    public class GenerateForgetPasswordValidator : AbstractValidator<GenerateForgetPasswordDto>
    {
        private readonly string nubmers = "0123456789+";
        public GenerateForgetPasswordValidator()
        {
            RuleFor(pr=>pr.Email)
                .NotEmpty()
                .EmailAddress()
                .MinimumLength(5)
                .MaximumLength(100);

            RuleFor(pr => pr.PhoneNumber)
                .NotNull()
                .Length(9, 10)
                .Custom(
                (value, context) =>
                {
                    var isContainsWrongDigits = false;
                    foreach (var item in value)
                    {
                        if (!nubmers.ToLower().Contains(item))
                        {
                            isContainsWrongDigits = true;
                            break;
                        }
                    }
                    if (isContainsWrongDigits)
                    {
                        context.AddFailure("Phone number has wrong digits");
                    }
                });
        }
    }
}
