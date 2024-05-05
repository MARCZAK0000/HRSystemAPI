using HumanResources.Domain.AbsenceModelDto;
using HumanResources.Domain.Entities;

namespace HumanResources.Domain.Repository
{
    public interface IAbsenceRepository
    {
        Task<Absence> CreateAbsenceAsync(CreateAbsenceDto createAbsence);

        Task<List<Absence>> ShowAbsencesByYearAsync(string userID, int year);

        Task<Absence> AbsenceDecisionAsync(AbsenceDecisionInfoDto infoDto);

        Task<MemoryStream> GenerateAbsenceReportPDF(List<AbsenceInfoDto> list, (string userID, int year) info); 
    }
}
