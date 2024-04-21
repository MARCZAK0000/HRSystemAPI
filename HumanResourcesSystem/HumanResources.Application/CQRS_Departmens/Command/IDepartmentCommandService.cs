using HumanResources.Domain.DepartmentModelDto;
using HumanResources.Domain.Response;

namespace HumanResources.Application.CQRS_Departmens.Command
{
    public interface IDepartmentCommandService
    {
        Task<DepartmentResponse> ChangeUserDepartmentAscyn(ChangeDepartmentDto changeDepartment);

        Task<DepartmentResponse> AddDepartmentAsync(DepartmentUpdateDto addDepartment);

        Task<DepartmentResponse> UpdateDepartmentAsync(DepartmentUpdateDto updateDepartment, int departmentID);
    }
}
