using FluentValidation.TestHelper;
using HumanResources.Domain.ModelDtos;
using Xunit;

namespace HumanResources.Domain.Validations.AccountValidations.Tests
{
    public class RegisterUserValidatorTests
    {
        [Fact()]
        public void RegisterUserValidator_ShouldNotHaveErrors()
        {
            var registerUserValidator = new RegisterUserValidator();

            var registerUserDto = new RegisterAccountAsyncDto()
            {
                PhoneNumber = "123456789",
                Email = "test@example.com",
                Password = "Password123",
                ConfirmPassword = "Password123"
            };


            var result = registerUserValidator.TestValidate(registerUserDto);

            result.ShouldNotHaveAnyValidationErrors();

        }

        [Fact()]
        public void RegisterUserValidator_ShouldHaveErrors()
        {
            var registerUserValidator = new RegisterUserValidator();

            var registerUserDto = new RegisterAccountAsyncDto()
            {
                PhoneNumber = "12345678a",
                Email = "test@example.com",
                Password = "Password12a",
                ConfirmPassword = "Password123"
            };


            var result = registerUserValidator.TestValidate(registerUserDto);

            result.ShouldHaveAnyValidationError();

        }

    }
}