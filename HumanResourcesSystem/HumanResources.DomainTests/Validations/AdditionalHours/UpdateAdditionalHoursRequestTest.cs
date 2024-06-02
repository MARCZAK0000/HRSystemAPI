using FluentValidation.TestHelper;
using HumanResources.Domain.AdditionalHoursDto;
using Xunit;

namespace HumanResources.Domain.Validations.AdditionalHours.Validations
{
    public class UpdateAdditionalHoursRequestTest
    {
        [Fact()]
        public void UpdateAdditionalHoursRequestTest_ShouldBeOK()
        {
            var dto = new UpdateAdditionalHoursRequestDto()
            {
                AdditionalHoursID=1,
                StartTime = DateTime.Now.AddHours(2),
                EndTime = DateTime.Now.AddHours(3),
                Title = "TEST",
                UserCode="00000000000000"
            };

            var validate = new UpdateAdditionalHoursRequestValidator();

            var result = validate.TestValidate(dto);

            result.ShouldNotHaveAnyValidationErrors();
        }


        [Fact()]
        public void UpdateAdditionalHoursRequestTest_ShouldHaveInvalidDate()
        {
            var dto = new UpdateAdditionalHoursRequestDto()
            {
                AdditionalHoursID = 1,
                StartTime = DateTime.Now.AddHours(2),
                EndTime = DateTime.Now.AddHours(1),
                Title = "TEST",
                UserCode = "00000000000000"
            };

            var validate = new UpdateAdditionalHoursRequestValidator();

            var result = validate.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(pr=>pr.EndTime);
        }

        [Fact()]
        public void UpdateAdditionalHoursRequestTest_ShouldHaveError()
        {
            var dto = new UpdateAdditionalHoursRequestDto()
            {
                AdditionalHoursID = -1,
                StartTime = DateTime.Now.AddHours(2),
                EndTime = DateTime.Now.AddHours(1),
                Title = "",
                UserCode = "00000000000000"
            };

            var validate = new UpdateAdditionalHoursRequestValidator();

            var result = validate.TestValidate(dto);

            result.ShouldHaveAnyValidationError();
        }

    }
}