using FluentValidation.TestHelper;
using HumanResources.Domain.AdditionalHoursDto;
using Xunit;

namespace HumanResources.Domain.Validations.AdditionalHours.Validations
{
    public class ShowAllAdditionalHoursTest
    {
        [Fact()]
        public void ShowAllAdditionalHoursValidatorTest_ShouldBeOK()
        {
            var dto = new ShowAllAdditionalHoursDto()
            {
                PageNumber = 1,
                PageSize = 10,
                UserCode = "00000000000",
            };

            var validator = new ShowAllAdditionalHoursValidator();

            var result = validator.TestValidate(dto);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void ShowAllAdditionalHoursValidatorTest_ShouldHaveError()
        {
            var dto = new ShowAllAdditionalHoursDto()
            {
                PageNumber = 1,
                PageSize = 0,
                UserCode = "00000000000",
            };

            var validator = new ShowAllAdditionalHoursValidator();

            var result = validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(pr=>pr.PageSize);

        }
    }
}