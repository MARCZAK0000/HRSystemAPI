using AutoMapper;
using HumanResources.Domain.DepartmentModelDto;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Repository;

namespace HumanResources.Application.CQRS_Departmens.Handler
{
    public class DepartmentHandlerService : IDepartmentHandlerService
    {
        private readonly IMapper _mapper;

        private readonly IDepartmentReposiotry _departmentRepository;

        public DepartmentHandlerService(IMapper mapper, IDepartmentReposiotry departmentRepository)
        {
            _mapper = mapper;
            _departmentRepository = departmentRepository;
        }

        public async Task<DepartmentInfoDto> DepartmentInfoAsync(int departmentId) => 
            _mapper.Map<DepartmentInfoDto>(await _departmentRepository.DepartmentInfoAsync(departmentId));
    }
}
