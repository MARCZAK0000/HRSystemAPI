using AutoMapper;
using HumanResources.Domain.AdditionalHoursDto;
using HumanResources.Domain.AdditionalHoursModel;
using HumanResources.Domain.Pagination;
using HumanResources.Domain.Repository;

namespace HumanResources.Application.CQRS_AdditionalHours.Handler
{
    public class AdditionalHoursHandlerServices : IAdditionalHoursHandlerServices
    {
        private readonly IMapper _mapper;

        private readonly IAdditionalHoursReposiotry _additionalHoursReposiotry;

        public AdditionalHoursHandlerServices(IMapper mapper, IAdditionalHoursReposiotry additionalHoursReposiotry)
        {
            _mapper = mapper;
            _additionalHoursReposiotry = additionalHoursReposiotry;
        }

        public async Task<PaginationBase<List<AdditionalHoursResponse>>> ShowAllAdditionalHoursRequestAsync(ShowAllAdditionalHoursDto hours, CancellationToken token)=>
            await _additionalHoursReposiotry.ShowAllAdditionalHoursRequestAsync(hours, token);
        
    }
}
