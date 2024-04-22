using FluentValidation;
using HumanResources.Domain.UserModelDto;

namespace HumanResources.Domain.Validations.AttendanceValidation
{
    public class UserArrivalValidator : AbstractValidator<UserArrivalDto>
    {
        public UserArrivalValidator()
        {
            RuleFor(pr => pr.ArrivalDate)
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
