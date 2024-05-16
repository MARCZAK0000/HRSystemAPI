using HumanResources.Domain.DepartmentModelDto;
using HumanResources.Domain.Response;

namespace HumanResources.Application.CQRS_Departmens.Command
{
    public interface IDepartmentCommandService
    {
        Task<DepartmentResponse> ChangeUserDeparmentAsync(ChangeDepartmentDto changeDepartment, CancellationToken token);

        Task<DepartmentResponse> AddDepartmentAsync(DepartmentUpdateDto addDepartment, CancellationToken token);

        Task<DepartmentResponse> UpdateDepartmentAsync(DepartmentUpdateDto updateDepartment, int departmentID, CancellationToken token);
    }
}
