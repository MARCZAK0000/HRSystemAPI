using HumanResources.Domain.ModelDtos;
using HumanResources.Domain.StorageAccountModel;
using Microsoft.AspNetCore.Http;

namespace HumanResources.Application.CQRS_User.Command
{
    public interface IUserCommandService
    {

        Task<bool> UpdateInfromationsAboutUserAsync(UpdateAccountInformationsDto updateAccountInformations, CancellationToken token);

        Task<bool> UpdateExperienceInformationsAboutUser(UpdateExperienceInfomrationsDto update, CancellationToken token);

        Task<BlobResponse> UploadUserImageAsync(IFormFile form, CancellationToken token);

        Task<BlobResponse> UpdateUserImageAsync(IFormFile form, CancellationToken token);
    }
}
