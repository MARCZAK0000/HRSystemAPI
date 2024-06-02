using FluentValidation.TestHelper;
using HumanResources.Domain.AdditionalHoursDto;
using Xunit;

namespace HumanResources.Domain.Validations.AdditionalHours.Validations
{
    public class ShowAdditionalHoursByIDTest
    {
        [Fact()]
        public void ShowAdditionalHoursByIDTest_ShouldBeOK()
        {
            var dto = new ShowAdditionalHoursDto()
            {
                AdditionalHoursID = 1,
                UserCode = "000000000000"
            };

            var validator = new ShowAdditionalHoursValidator(); 

            var result = validator.TestValidate(dto);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void ShowAdditionalHoursByIDTest_ShouldHaveError()
        {
            var dto = new ShowAdditionalHoursDto()
            {
                AdditionalHoursID = 1,
            };

            var validator = new ShowAdditionalHoursValidator();

            var result = validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(pr=>pr.UserCode);
        }
    }
}