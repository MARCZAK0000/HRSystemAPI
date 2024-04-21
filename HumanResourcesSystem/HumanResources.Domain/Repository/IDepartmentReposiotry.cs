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
        Task<DepartmentResponse> ChangeUserDepartmentAsync (ChangeDepartmentDto changeDepartment);

        Task<Departments> DepartmentInfoAsync(int departmentId);    

        Task<DepartmentResponse> AddDepartmentAsync(DepartmentUpdateDto departmentInfo);

        Task<DepartmentResponse> UpdateDepartmentAsync(DepartmentUpdateDto changeDepartment, int depratmentID);
    }
}
