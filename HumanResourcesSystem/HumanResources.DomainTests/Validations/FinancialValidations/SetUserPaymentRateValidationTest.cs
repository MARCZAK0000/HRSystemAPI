using FluentValidation.TestHelper;
using HumanResources.Domain.FinancialModelDto;
using HumanResources.Domain.Validations.FinancialValidations;
using Xunit;

namespace HumanResources.DomainTests.Validations.FinancialValidations
{
    public class SetUserPaymentRateValidationTest
    {
        [Fact]
        public void SetUserPaymentRateValidation_ShouldBeOK()
        {
            var validator = new SetUserPaymentRateValidator();

            var dto = new SetUserPaymentRateDto()
            {
                RatePLN = 25,
                UserCode = "0000"
            };

            var result = validator.TestValidate(dto);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void SetUserPaymentRateValidation_ShouldBeNotOK()
        {
            var validator = new SetUserPaymentRateValidator();

            var dto = new SetUserPaymentRateDto()
            {
                RatePLN = 19,
                UserCode = ""
            };

            var result = validator.TestValidate(dto);

            result.ShouldHaveAnyValidationError();
        }
    }
}
