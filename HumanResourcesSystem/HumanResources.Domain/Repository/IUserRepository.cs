using HumanResources.Domain.Entities;
using HumanResources.Domain.ModelDtos;
using HumanResources.Domain.StorageAccountModel;
using Microsoft.AspNetCore.Http;

namespace HumanResources.Domain.Repository
{
    public interface IUserRepository
    {
        Task<UserInfo> GetInfromationsAboutUserAsync(CancellationToken token);

        Task<bool> UpdateInformationsAboutUserAsync(UpdateAccountInformationsDto updateAccountInformationsDto, CancellationToken token);

        Task<bool> UpdateExperienceInformationsAboutUser(UpdateExperienceInfomrationsDto update, CancellationToken token);

        Task<BlobResponse> UploadUserImageAsync(IFormFile form, CancellationToken token);

        Task<BlobResponse> UpdateUserImageAsync(IFormFile form, CancellationToken token);
    }
}
