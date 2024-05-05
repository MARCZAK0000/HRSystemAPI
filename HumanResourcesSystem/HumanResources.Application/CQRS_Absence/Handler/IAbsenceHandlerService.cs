using HumanResources.Domain.AbsenceModelDto;

namespace HumanResources.Application.CQRS_Absence.Handler
{
    public interface IAbsenceHandlerService
    {
        Task<List<AbsenceInfoDto>> ShowAbsencesByYearAsync(string userID, int year);

        Task<MemoryStream> GeneratePdfReportAsync(string userID, int year);
    }
}
