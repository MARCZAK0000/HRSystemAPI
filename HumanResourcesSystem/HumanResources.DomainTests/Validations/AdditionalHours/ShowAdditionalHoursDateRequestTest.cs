using FluentValidation.TestHelper;
using HumanResources.Domain.AdditionalHoursDto;
using Xunit;

namespace HumanResources.Domain.Validations.AdditionalHours.Validations
{
    public class ShowAdditionalHoursDateRequestTest
    {
        [Fact()]
        public void ShowAdditionalHoursDateRequestTest_ShouldBeOK()
        {
            var dto = new ShowAdditionalHoursDateDto()
            {
                From = DateTime.Now.AddDays(-1),
                To = DateTime.Now.AddDays(1),
                PageSize = 1,
                PageNumber = 1,
                UserCode = "0000000000000"
            };

            var validator = new ShowAdditionalHoursDateValidator(); 

            var result = validator.TestValidate(dto);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void ShowAdditionalHoursDateRequestTest_ShouldBeError()
        {
            var dto = new ShowAdditionalHoursDateDto()
            {
                From = DateTime.Now.AddDays(3),
                To = DateTime.Now.AddDays(1),
                PageSize = 1,
                PageNumber = 1,
                UserCode = "00000000000000"
            };

            var validator = new ShowAdditionalHoursDateValidator();

            var result = validator.TestValidate(dto);

            result.ShouldHaveAnyValidationError();
        }
    }
}