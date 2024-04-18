using AutoMapper;
using HumanResources.Domain.Repository;

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
    }
}
