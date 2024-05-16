using AutoMapper;
using HumanResources.Domain.DepartmentModelDto;
using HumanResources.Domain.Repository;
using HumanResources.Domain.Response;

namespace HumanResources.Application.CQRS_Departmens.Command
{
    public class DepartmentCommandService : IDepartmentCommandService
    {
        private readonly IMapper _mapper;

        private readonly IDepartmentReposiotry _departmentRepository;

        public DepartmentCommandService(IMapper mapper, IDepartmentReposiotry departmentRepository)
        {
            _mapper = mapper;
            _departmentRepository = departmentRepository;
        }


        public async Task<DepartmentResponse> ChangeUserDeparmentAsync(ChangeDepartmentDto changeDepartment, CancellationToken token) =>
            await _departmentRepository.ChangeUserDepartmentAsync(changeDepartment, token);


        public async Task<DepartmentResponse> AddDepartmentAsync(DepartmentUpdateDto addDepartment, CancellationToken token) =>
            await _departmentRepository.AddDepartmentAsync(addDepartment, token);

        public async Task<DepartmentResponse> UpdateDepartmentAsync(DepartmentUpdateDto departmentUpdate, int departmentID, CancellationToken token) =>
            await _departmentRepository.UpdateDepartmentAsync(departmentUpdate, departmentID, token);
    }
}
