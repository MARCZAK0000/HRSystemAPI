using FluentValidation.TestHelper;
using HumanResources.Domain.AdditionalHoursDto;
using Xunit;

namespace HumanResources.Domain.Validations.AdditionalHours.Validations
{
    public class DeleteAdditionalHoursRequestTest
    {
        [Fact()]
        public void DeleteAdditionalHoursRequestTest_ShouldBeOK()
        {
            var dto = new DeleteAdditionalHoursRequestDto()
            {
                AdditionalHoursID = 1,
                UserCode = "0000000000000"
            };

            var validate = new DeleteAdditionalHoursRequestValidator();

            var result = validate.TestValidate(dto);

            result.ShouldNotHaveAnyValidationErrors();

        }

        [Fact()]
        public void DeleteAdditionalHoursRequestTest_ShouldHaveError()
        {
            var dto = new DeleteAdditionalHoursRequestDto()
            {
                AdditionalHoursID = 1,
            };

            var validate = new DeleteAdditionalHoursRequestValidator();

            var result = validate.TestValidate(dto);

            result.ShouldHaveAnyValidationError();

        }
    }
}