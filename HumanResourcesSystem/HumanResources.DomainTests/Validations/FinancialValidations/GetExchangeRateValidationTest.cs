using FluentValidation.TestHelper;
using HumanResources.Domain.FinancialModelDto;
using HumanResources.Domain.Validations.FinancialValidations;
using Xunit;

namespace HumanResources.DomainTests.Validations.FinancialValidations
{
    public class GetExchangeRateValidationTest
    {
        [Fact]
        public void GetExchangeRateValidationTest_ShoulBeOK()
        {
            var validator = new GetExchangeRateValidator();

            var dto = new GetExchangeRateAsyncDto()
            {
                CurrencyCode = "PLN"
            };

            var result = validator.TestValidate(dto);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void GetExchangeRateValidationTest_ShoulNotBeOK()
        {
            var validator = new GetExchangeRateValidator();

            var dto = new GetExchangeRateAsyncDto()
            {
                CurrencyCode = "XD"
            };

            var result = validator.TestValidate(dto);

            result.ShouldHaveAnyValidationError();
        }


    }
}
