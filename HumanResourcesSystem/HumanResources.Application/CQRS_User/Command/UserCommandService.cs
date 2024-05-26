﻿using HumanResources.Domain.ModelDtos;
using HumanResources.Domain.Repository;

namespace HumanResources.Application.CQRS_User.Command
{
    public class UserCommandService : IUserCommandService
    {

        private readonly IUserRepository _userRepository;

        public UserCommandService(IUserRepository accountReposiotry)
        {
            _userRepository = accountReposiotry;
        }

        public async Task<bool> UpdateExperienceInformationsAboutUser(UpdateExperienceInfomrationsDto update, CancellationToken token) =>
            await _userRepository.UpdateExperienceInformationsAboutUser(update, token);

        public async Task<bool> UpdateInfromationsAboutUserAsync(UpdateAccountInformationsDto updateAccountInformations, CancellationToken token) =>
            await _userRepository.UpdateInformationsAboutUserAsync(updateAccountInformations, token);


    }
}
