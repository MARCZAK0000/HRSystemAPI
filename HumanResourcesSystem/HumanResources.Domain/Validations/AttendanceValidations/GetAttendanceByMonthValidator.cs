using FluentValidation;
using HumanResources.Domain.AttendanceModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.Validations.AttendanceValidation
{
    public class GetAttendanceByMonthValidator : AbstractValidator<GetAttendanceByMonthDto>
    {
        public GetAttendanceByMonthValidator()
        {
            RuleFor(pr => pr.Month)
                .NotEmpty()
                .IsInEnum();

            RuleFor(pr => pr.Year)
                .NotEmpty()
                .GreaterThanOrEqualTo(2000)
                .LessThanOrEqualTo(2100);
        }
    }
}
