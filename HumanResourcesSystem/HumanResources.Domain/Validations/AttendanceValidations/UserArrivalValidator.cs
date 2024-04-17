using FluentValidation;
using HumanResources.Domain.UserModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.Validations.AttendanceValidation
{
    public class UserArrivalValidator:AbstractValidator<UserArrivalDto>
    {
        public UserArrivalValidator()
        {
            RuleFor(pr=>pr.ArrivalDate)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var currentDateMin = DateTime.Now.AddHours(-1);
                    var currentDateMax = DateTime.Now.AddHours(1);
                    if(currentDateMin > value || currentDateMax < value)
                    {
                        context.AddFailure("Wrong Date");
                    }
                });
        }
    }
}
