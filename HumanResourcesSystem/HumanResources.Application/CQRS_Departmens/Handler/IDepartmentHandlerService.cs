using HumanResources.Domain.DepartmentModelDto;

namespace HumanResources.Application.CQRS_Departmens.Handler
{
    public interface IDepartmentHandlerService
    {
        Task<DepartmentInfoDto> DepartmentInfoAsync(int  departmentId);
    }
}
