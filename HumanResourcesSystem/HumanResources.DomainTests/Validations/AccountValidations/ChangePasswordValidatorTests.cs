using FluentValidation.TestHelper;
using HumanResources.Domain.ModelDtos;
using Xunit;

namespace HumanResources.Domain.Validations.AccountValidations.Tests
{
    public class ChangePasswordValidatorTests
    {
        [Fact()]
        public void ChangePasswordValidator_ShouldNotHaveErrors()
        {
            var changePasswordValidator = new ChangePasswordValidator();

            var changePasswordDto = new ChangePasswordAsyncDto()
            {
                Password = "password",
                ConfirmPassword = "password",
                NewPassword = "password1",
                ConfirmNewPassword = "password1",
            };

            var result = changePasswordValidator.TestValidate(changePasswordDto);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void ChangePasswordValidator_ShouldHaveErrors()
        {
            var changePasswordValidator = new ChangePasswordValidator();

            var changePasswordDto = new ChangePasswordAsyncDto()
            {
                Password = "password",
                ConfirmPassword = "password1",
                NewPassword = "password1",
                ConfirmNewPassword = "password2",
            };

            var result = changePasswordValidator.TestValidate(changePasswordDto);

            result.ShouldHaveAnyValidationError();
        }
    }
}