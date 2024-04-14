using HumanResources.Domain.AbsenceModelDto;
using HumanResources.Domain.Entities;

namespace HumanResources.Domain.Repository
{
    public interface IAbsenceRepository
    {
        Task<Absence> CreateAbsenceAsync(CreateAbsenceDto createAbsence);
    }
}
