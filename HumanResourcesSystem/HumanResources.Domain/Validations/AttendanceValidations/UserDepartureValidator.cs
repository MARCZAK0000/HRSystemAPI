using FluentValidation;
using HumanResources.Domain.UserModelDto;

namespace HumanResources.Domain.Validations.AttendanceValidation
{
    public class UserDepartureValidator : AbstractValidator<UserDepartureDto>
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
