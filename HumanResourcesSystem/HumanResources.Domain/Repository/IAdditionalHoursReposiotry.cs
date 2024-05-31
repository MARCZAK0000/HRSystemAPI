using HumanResources.Domain.AdditionalHoursDto;
using HumanResources.Domain.AdditionalHoursModel;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Pagination;

namespace HumanResources.Domain.Repository
{
    public interface IAdditionalHoursReposiotry
    {
        Task<AdditionalHoursResponse> CreateAdditionalHoursRequestAsync(CreateAdditionalHoursRequestDto hours, CancellationToken token);

        Task<AdditionalHoursResponse> UpdateAdditionalHoursRequestAsync(UpdateAdditionalHoursRequestDto hours, CancellationToken token);

        Task<PaginationBase<List<AdditionalHoursResponse>>> 
            ShowAllAdditionalHoursRequestAsync(ShowAllAdditionalHoursDto hours, CancellationToken token);

    }
}
