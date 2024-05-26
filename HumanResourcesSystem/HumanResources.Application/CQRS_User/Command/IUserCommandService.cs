using HumanResources.Domain.ModelDtos;

namespace HumanResources.Application.CQRS_User.Command
{
    public interface IUserCommandService
    {

        Task<bool> UpdateInfromationsAboutUserAsync(UpdateAccountInformationsDto updateAccountInformations, CancellationToken token);

        Task<bool> UpdateExperienceInformationsAboutUser(UpdateExperienceInfomrationsDto update, CancellationToken token);
    }
}
