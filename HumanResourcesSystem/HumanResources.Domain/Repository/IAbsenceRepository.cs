using HumanResources.Domain.AbsenceModelDto;
using HumanResources.Domain.Entities;

namespace HumanResources.Domain.Repository
{
    public interface IAbsenceRepository
    {
        Task<Absence> CreateAbsenceAsync(CreateAbsenceDto createAbsence, CancellationToken token);

        Task<List<Absence>> ShowAbsencesByYearAsync(string userCode, int year, CancellationToken token);

        Task<Absence> AbsenceDecisionAsync(AbsenceDecisionInfoDto infoDto, CancellationToken token);

        Task<Absence> ShowAbsenceAsync(string userCode, int absenceId, CancellationToken token);

        Task<MemoryStream> GenerateAbsenceReportPDF(List<AbsenceInfoDto> list, (string userID, int year) info, CancellationToken token); 
    }
}
