using HumanResources.Domain.DepartmentModelDto;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.Repository
{
    public interface IDepartmentReposiotry
    {
        Task<DepartmentResponse> ChangeUserDepartmentAsync (ChangeDepartmentDto changeDepartment, CancellationToken token);

        Task<Departments> DepartmentInfoAsync(int departmentId, CancellationToken token);    

        Task<DepartmentResponse> AddDepartmentAsync(DepartmentUpdateDto departmentInfo, CancellationToken token);

        Task<DepartmentResponse> UpdateDepartmentAsync(DepartmentUpdateDto changeDepartment, int depratmentID, CancellationToken token);

        Task<List<Departments>> GetAllDeparmentsAsync(CancellationToken token);
    }
}
