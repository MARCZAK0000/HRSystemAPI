using FluentValidation.TestHelper;
using HumanResources.Domain.AdditionalHoursDto;
using Xunit;

namespace HumanResources.Domain.Validations.AdditionalHours.Validations
{
    public class CreateAdditionalHoursRequestTest
    {
        [Fact()]
        public void CreateAdditionalHoursRequestTest_ShouldBeOK()
        {
            var dto = new CreateAdditionalHoursRequestDto()
            {
                StartTime = DateTime.Now.AddHours(9),
                EndTime = DateTime.Now.AddHours(10),
                Title = "Title",
                UserCode = "0000000000000"
            };

            var validate = new CreateAdditionalHoursRequestValidator();

            var result = validate.TestValidate(dto);
            
            result.ShouldNotHaveAnyValidationErrors();
        }


        [Fact()]
        public void CreateAdditionalHoursRequestTest_ShouldHaveError()
        {
            var dto = new CreateAdditionalHoursRequestDto()
            {
                StartTime = DateTime.Now.AddHours(11),
                EndTime = DateTime.Now.AddHours(10),
                Title = "Title",
                UserCode = "0000000000000"
            };

            var validate = new CreateAdditionalHoursRequestValidator();

            var result = validate.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(pr=>pr.StartTime);
            result.ShouldHaveValidationErrorFor(pr=>pr.EndTime);
        }
    }
}