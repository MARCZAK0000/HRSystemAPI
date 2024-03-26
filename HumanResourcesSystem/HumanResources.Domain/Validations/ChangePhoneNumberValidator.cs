using FluentValidation;
using HumanResources.Domain.ModelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.Validations
{
    public class ChangePhoneNumberValidator:AbstractValidator<ChangePhoneNumberDto>
    {
        private readonly string nubmers = "0123456789+";
        public ChangePhoneNumberValidator()
        {
            RuleFor(pr=>pr.PhoneNumber)
                .NotNull()
                .Length(9,10)
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
                    if(isContainsWrongDigits) 
                    {   
                        context.AddFailure("Phone number has wrong digits");
                    }
                });


            RuleFor(pr => pr.ConfirmPhoneNumber)
                .Equal(pr => pr.PhoneNumber);
        }
    }
}
