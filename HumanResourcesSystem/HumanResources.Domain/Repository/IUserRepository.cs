using HumanResources.Domain.Entities;
using HumanResources.Domain.ModelDtos;
using HumanResources.Domain.UserModelDto;

namespace HumanResources.Domain.Repository
{
    public interface IUserRepository
    {
        Task<UserInfo> GetInfromationsAboutUserAsync(CancellationToken token);

        Task<bool> UpdateInformationsAboutUserAsync(UpdateAccountInformationsDto updateAccountInformationsDto, CancellationToken token);

    }
}
