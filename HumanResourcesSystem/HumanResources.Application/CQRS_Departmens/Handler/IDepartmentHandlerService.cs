using HumanResources.Domain.DepartmentModelDto;

namespace HumanResources.Application.CQRS_Departmens.Handler
{
    public interface IDepartmentHandlerService
    {
        Task<DepartmentInfoDto> DepartmentInfoAsync(int  departmentId, CancellationToken token);

        Task<List<DepartmentInfoDto>> GetAllDepartmenst(CancellationToken token);

    }
}
