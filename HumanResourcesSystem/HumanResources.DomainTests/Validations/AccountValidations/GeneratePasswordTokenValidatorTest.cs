using FluentValidation.TestHelper;
using HumanResources.Domain.ModelDtos;
using HumanResources.Domain.Validations.AccountValidations;
using Xunit;

namespace HumanResources.DomainTests.Validations.AccountValidations
{
    public class GeneratePasswordTokenValidatorTest
    {
        [Fact()]
        public void GeneratePasswordTokenValidator_ShouldNotHaveErrors()
        {
            var tokenValidator = new GenerateForgetPasswordValidator();

            var token = new GenerateForgetPasswordDto()
            {
                Email = "jj.marczak98@gmail.com",
                PhoneNumber = "737410216",
            };

            var result = tokenValidator.TestValidate(token);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void ChangePhoneValidator_ShouldHaveErrors()
        {
            var tokenValidator = new GenerateForgetPasswordValidator();

            var token = new GenerateForgetPasswordDto()
            {
                Email = "jj.marczak98@gmail.com",
                PhoneNumber = "73a410216",
            };

            var result = tokenValidator.TestValidate(token);

            result.ShouldHaveAnyValidationError();
        }
    }
}
