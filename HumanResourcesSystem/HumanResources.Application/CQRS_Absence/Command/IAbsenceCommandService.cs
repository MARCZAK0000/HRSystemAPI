using HumanResources.Domain.AbsenceModelDto;

namespace HumanResources.Application.CQRS_Absence.Command
{
    public interface IAbsenceCommandService
    {
        Task<AbsenceInfoDto> CreateAbsenceAsync(CreateAbsenceDto createAbsence, CancellationToken token);

        Task<AbsenceDecisionDto> AbsenceDecisionAsync (AbsenceDecisionInfoDto absenceDecision, CancellationToken token);

    }
}
