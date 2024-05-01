using AutoMapper;
using HumanResources.Domain.Cache;
using HumanResources.Domain.DepartmentModelDto;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Repository;
using Microsoft.Extensions.Caching.Memory;

namespace HumanResources.Application.CQRS_Departmens.Handler
{
    public class DepartmentHandlerService : IDepartmentHandlerService
    {
        private readonly IMapper _mapper;

        private readonly IDepartmentReposiotry _departmentRepository;

        private readonly IMemoryCache _cache;

        public DepartmentHandlerService(IMapper mapper, IDepartmentReposiotry departmentRepository,
            IMemoryCache cache)
        {
            _mapper = mapper;
            _departmentRepository = departmentRepository;
            _cache = cache;
        }

        public async Task<DepartmentInfoDto> DepartmentInfoAsync(int departmentId) =>
            _mapper.Map<DepartmentInfoDto>(await _departmentRepository.DepartmentInfoAsync(departmentId));

        public async Task<List<DepartmentInfoDto>> GetAllDepartmenst()
        {
            if (!_cache.TryGetValue(CacheKeys.GetAllDeparmentsCache, out var departmentInfo))
            {
                departmentInfo = await _departmentRepository.GetAllDeparmentsAsync();
                var options = CacheOptions.FastOptions();
                _cache.Set(CacheKeys.GetAllDeparmentsCache, departmentInfo, options);
            }
            return _mapper.Map<List<DepartmentInfoDto>>(departmentInfo);
        }
            
    }
}
