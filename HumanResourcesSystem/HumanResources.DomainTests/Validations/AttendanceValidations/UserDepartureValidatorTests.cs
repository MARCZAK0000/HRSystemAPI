using FluentValidation.TestHelper;
using HumanResources.Domain.UserModelDto;
using Xunit;

namespace HumanResources.Domain.Validations.AttendanceValidation.Tests
{
    public class UserDepartureValidatorTests
    {
        [Fact()]
        public void UserDepartureValidator_ShouldNotHaveAnyErrors()
        {
            var userDeparureValidator = new UserDepartureValidator();

            var userDepartureDto = new UserDepartureDto()
            {
                Id = 1,
                DepartureDate = DateTime.Now,
            };

            var result = userDeparureValidator.TestValidate(userDepartureDto);

            result.ShouldNotHaveAnyValidationErrors();  

        }


        [Fact()]
        public void UserDepartureValidator_ShouldHaveAnyErrors()
        {
            var userDeparureValidator = new UserDepartureValidator();

            var userDepartureDto = new UserDepartureDto()
            {
                Id = 1,
                DepartureDate = DateTime.Now.AddHours(3),
            };

            var result = userDeparureValidator.TestValidate(userDepartureDto);

            result.ShouldHaveAnyValidationError();

        }
    }
}