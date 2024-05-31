using HumanResources.Domain.AdditionalHoursDto;
using HumanResources.Domain.AdditionalHoursModel;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Pagination;

namespace HumanResources.Application.CQRS_AdditionalHours.Handler
{
    public interface IAdditionalHoursHandlerServices
    {
        Task<PaginationBase<List<AdditionalHoursResponse>>> 
            ShowAllAdditionalHoursRequestAsync(ShowAllAdditionalHoursDto hours, CancellationToken token);

        Task<PaginationBase<List<AdditionalHoursResponse>>>
            ShowAdditionalHoursRequestByDateAsync(ShowAdditionalHoursDateDto hours, CancellationToken token);

        Task<AdditionalHoursResponse> 
            ShowAdditionalHoursRequestByIDAsync(ShowAdditionalHoursDto hours, CancellationToken token);
    }
}
