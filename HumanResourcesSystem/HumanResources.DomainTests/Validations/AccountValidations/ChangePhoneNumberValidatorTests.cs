using FluentValidation.TestHelper;
using HumanResources.Domain.ModelDtos;
using Xunit;

namespace HumanResources.Domain.Validations.AccountValidations.Tests
{
    public class ChangePhoneNumberValidatorTests
    {
        [Fact()]
        public void ChanePhoneValidator_ShouldNotHaveErrors()
        {
            var phoneValidator = new ChangePhoneNumberValidator();

            var phoneDto = new ChangePhoneNumberDto()
            {
                PhoneNumber = "737410216",
                ConfirmPhoneNumber = "737410216"
            };

            var result = phoneValidator.TestValidate(phoneDto);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void ChangePhoneValidator_ShouldHaveErrors()
        {
            var phoneValidator = new ChangePhoneNumberValidator();

            var phoneDto = new ChangePhoneNumberDto()
            {
                PhoneNumber = "73741021a",
                ConfirmPhoneNumber = "73741021a"
            };

            var result = phoneValidator.TestValidate(phoneDto);

            result.ShouldHaveAnyValidationError();
        }
    }
}