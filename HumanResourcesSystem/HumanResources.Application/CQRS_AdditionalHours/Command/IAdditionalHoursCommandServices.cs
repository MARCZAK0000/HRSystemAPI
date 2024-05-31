using HumanResources.Domain.AdditionalHoursDto;
using HumanResources.Domain.AdditionalHoursModel;

namespace HumanResources.Application.CQRS_AdditionalHours.Command
{
    public interface IAdditionalHoursCommandServices
    {
        Task<AdditionalHoursResponse> CreateAdditionalHoursRequestAsync(CreateAdditionalHoursRequestDto hours, CancellationToken token);

        Task<AdditionalHoursResponse> UpdateAdditionalHoursRequestAsync(UpdateAdditionalHoursRequestDto hours, CancellationToken token);

        Task<AdditionalHoursResponse> DeleteAdditionalHoursRequestAsync(DeleteAdditionalHoursRequestDto hours, CancellationToken token);
    }
}
