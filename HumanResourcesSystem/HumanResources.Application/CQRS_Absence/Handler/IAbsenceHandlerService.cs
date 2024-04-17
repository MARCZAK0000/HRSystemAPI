using HumanResources.Domain.AbsenceModelDto;

namespace HumanResources.Application.CQRS_Absence.Handler
{
    public interface IAbsenceHandlerService
    {
        Task<List<AbsenceInfoDto>> ShowAbsencesByYearAsync(int year);
    }
}
