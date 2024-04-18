using AutoMapper;
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
    }
}
