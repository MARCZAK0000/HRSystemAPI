using HumanResources.Domain.Entities;
using HumanResources.Domain.ModelDtos;
using HumanResources.Domain.UserModelDto;

namespace HumanResources.Domain.Repository
{
    public interface IUserRepository
    {
        Task<UserInfo> GetInfromationsAboutUserAsync();

        Task<bool> UpdateInformationsAboutUserAsync(UpdateAccountInformationsDto updateAccountInformationsDto);

    }
}
