using HumanResources.Domain.AbsenceModelDto;

namespace HumanResources.Application.CQRS_Absence.Handler
{
    public interface IAbsenceHandlerService
    {
        Task<List<AbsenceInfoDto>> ShowAbsencesByYearAsync(string userCode, int year, CancellationToken token);

        Task<AbsenceInfoDto> ShowAbsenceByIDAsync(string userCode, int absenceId, CancellationToken token);

        Task<MemoryStream> GeneratePdfReportAsync(string userID, int year, CancellationToken token);
    }
}
