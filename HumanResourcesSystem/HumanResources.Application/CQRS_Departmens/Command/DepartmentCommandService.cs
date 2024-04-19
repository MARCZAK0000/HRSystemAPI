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

        public async Task<DepartmentResponse> ChangeUserDepartmentAscyn(ChangeDepartmentDto changeDepartment) =>
            await _departmentRepository.ChangeUserDepartmentAsync(changeDepartment);
    }
}
