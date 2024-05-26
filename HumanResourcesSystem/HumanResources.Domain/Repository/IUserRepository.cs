using HumanResources.Domain.Entities;
using HumanResources.Domain.ModelDtos;

namespace HumanResources.Domain.Repository
{
    public interface IUserRepository
    {
        Task<UserInfo> GetInfromationsAboutUserAsync(CancellationToken token);

        Task<bool> UpdateInformationsAboutUserAsync(UpdateAccountInformationsDto updateAccountInformationsDto, CancellationToken token);

        Task<bool> UpdateExperienceInformationsAboutUser(UpdateExperienceInfomrationsDto update, CancellationToken token);
    }
}
