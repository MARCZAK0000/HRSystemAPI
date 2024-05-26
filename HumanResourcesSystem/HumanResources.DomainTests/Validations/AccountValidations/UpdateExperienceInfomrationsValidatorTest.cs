using FluentValidation.TestHelper;
using HumanResources.Domain.ModelDtos;
using Xunit;

namespace HumanResources.Domain.Validations.AccountValidations.Validations
{
    public class UpdateExperienceInfomrationsValidatorTest
    {
        [Fact()]
        public void UpdateExperienceInfomrationsValidator_ShouldBeOK()
        {
            var dto = new UpdateExperienceInfomrationsDto()
            {
                YearsOfExperience = 8,
                Level = Enums.EducationLevel.Higher
            };

            var validator = new UpdateExperienceInfomrationsValidator();

            var result = validator.TestValidate(dto);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void UpdateExperienceInfomrationsValidator_ShouldBeAnError()
        {
            var dto = new UpdateExperienceInfomrationsDto()
            {
                YearsOfExperience = 8
            };

            var validator = new UpdateExperienceInfomrationsValidator();

            var result = validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(pr => pr.Level);
        }
    }
}