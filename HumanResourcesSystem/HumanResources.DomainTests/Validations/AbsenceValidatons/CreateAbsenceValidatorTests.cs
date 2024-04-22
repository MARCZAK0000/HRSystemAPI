using FluentValidation.TestHelper;
using HumanResources.Domain.AbsenceModelDto;
using Xunit;

namespace HumanResources.Domain.Validations.AttendanceValidatons.Tests
{
    public class CreateAbsenceValidatorTests
    {
        [Fact()]
        public void CreateAbsenceValidator_Should_NoErrors()
        {

            //arrange
            var createAbsenceValidator = new CreateAbsenceValidator();

            var createAbsenceDto = new CreateAbsenceDto()
            {
                AbsenceTypeId = 1,
                Name = "Test",
                CreatedTime = DateTime.UtcNow,
                StartTime = DateTime.UtcNow.AddDays(1),
                EndTime = DateTime.UtcNow.AddDays(2)
            };


            //act
            var result = createAbsenceValidator.TestValidate(createAbsenceDto);
            //arr
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void CreateAbsenceValidator_ShouldHaveErrors()
        {
            var createAbsenceValidator = new CreateAbsenceValidator();

            var createAbsenceDto = new CreateAbsenceDto()
            {
                AbsenceTypeId = 10,
                Name = "TEST",
                CreatedTime = DateTime.UtcNow,
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddDays(-2)
            };



            var result = createAbsenceValidator.TestValidate(createAbsenceDto);


            result.ShouldHaveAnyValidationError();
        }
    }
}