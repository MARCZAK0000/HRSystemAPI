using FluentValidation.TestHelper;
using HumanResources.Domain.AbsenceModelDto;
using HumanResources.Domain.Validations.AbsenceValidatons;
using Xunit;

namespace HumanResources.DomainTests.Validations.AbsenceValidatons
{
    public class ShowAbsenceByIdValidatorTest
    {
        [Fact()]
        public void ShowAbsenceByIDValidator_ShouldBeOK()
        {
            var dto = new ShowAbsenceByIdDto()
            {
                AbsenceId = 1,
                UserCode = "000000000000"
            };

            var validation = new ShowAbsenceByIdValidator();

            var result = validation.TestValidate(dto);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void ShowAbsenceByIDValidator_ShouldBeAnError()
        {
            var dto = new ShowAbsenceByIdDto()
            {
                AbsenceId = 1,
                UserCode = "0000000"
            };

            var validation = new ShowAbsenceByIdValidator();

            var result = validation.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(pr => pr.UserCode);
        }
    }
}
