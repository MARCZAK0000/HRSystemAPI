using AutoMapper;
using HumanResources.Domain.AdditionalHoursDto;
using HumanResources.Domain.AdditionalHoursModel;
using HumanResources.Domain.Repository;

namespace HumanResources.Application.CQRS_AdditionalHours.Command
{
    public class AdditionalHoursCommandServices : IAdditionalHoursCommandServices
    {
        private readonly IMapper _mapper;

        private readonly IAdditionalHoursReposiotry _additionalHoursReposiotry;

        public AdditionalHoursCommandServices(IMapper mapper, IAdditionalHoursReposiotry additionalHoursReposiotry)
        {
            _mapper = mapper;
            _additionalHoursReposiotry = additionalHoursReposiotry;
        }

        public async Task<AdditionalHoursResponse> CreateAdditionalHoursRequestAsync(CreateAdditionalHoursRequestDto hours, CancellationToken token) => 
            await _additionalHoursReposiotry.CreateAdditionalHoursRequestAsync(hours, token);

        public async Task<AdditionalHoursResponse> UpdateAdditionalHoursRequestAsync(UpdateAdditionalHoursRequestDto hours, CancellationToken token) => 
            await _additionalHoursReposiotry.UpdateAdditionalHoursRequestAsync(hours, token);

        public async Task<AdditionalHoursResponse> DeleteAdditionalHoursRequestAsync(DeleteAdditionalHoursRequestDto hours, CancellationToken token) => 
            await _additionalHoursReposiotry.DeleteAdditionalHoursRequestAsync(hours, token);

    }
}
