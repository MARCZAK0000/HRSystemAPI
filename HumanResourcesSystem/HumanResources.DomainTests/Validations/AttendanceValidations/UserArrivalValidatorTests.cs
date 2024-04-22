using Xunit;
using HumanResources.Domain.Validations.AttendanceValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HumanResources.Domain.UserModelDto;
using FluentValidation.TestHelper;

namespace HumanResources.Domain.Validations.AttendanceValidation.Tests
{
    public class UserArrivalValidatorTests
    {
        [Fact()]
        public void UserArrivalValidator_ShouldNotHaveAnyErrors()
        {
            var userArrivalValidator = new UserArrivalValidator();

            var userArrivalDto = new UserArrivalDto()
            {
                ArrivalDate = DateTime.Now,
            };

            var result = userArrivalValidator.TestValidate(userArrivalDto);

            result.ShouldNotHaveAnyValidationErrors();
        }


        [Fact()]
        public void UserArrivalValidator_ShouldHaveAnyErrors()
        {
            var userArrivalValidator = new UserArrivalValidator();

            var userArrivalDto = new UserArrivalDto()
            {
                ArrivalDate = DateTime.Now.AddHours(2),
            };

            var result = userArrivalValidator.TestValidate(userArrivalDto);

            result.ShouldHaveAnyValidationError();
        }
    }


}