﻿using FluentValidation;
using HumanResources.Domain.UserModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.Validations.AttendanceValidation
{
    public class UserDepartureValidator:AbstractValidator<UserDepartureDto>
    {
        public UserDepartureValidator()
        {
            RuleFor(pr => pr.DepartureDate)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var currentDateMin = DateTime.Now.AddHours(-1);
                    var currentDateMax = DateTime.Now.AddHours(1);
                    if (currentDateMin > value || currentDateMax < value)
                    {
                        context.AddFailure("Wrong Date");
                    }
                });
        }
    }
}
