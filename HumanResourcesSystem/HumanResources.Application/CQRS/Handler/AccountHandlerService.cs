﻿using HumanResources.Application.CQRS.IUserHandler;
using HumanResources.Domain.EmailModelDto;
using HumanResources.Domain.ModelDtos;
using HumanResources.Domain.Repository;
using HumanResources.Domain.Response;

namespace HumanResources.Application.CQRS.UserHandler
{
    public class AccountHandlerService : IAccountHandlerService
    {

        private readonly IAccountReposiotry _accountRepository;

        public AccountHandlerService(IAccountReposiotry accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<EmailResponseDto> GenerateConfirmEmailTokenAsync() =>
            await _accountRepository.GenerateConfirmEmailTokenAsync();

        public async Task<UserResponse> GenerateForgetPasswordTokenAsync(string email, string phonenumber) =>
            await _accountRepository.GenerateForgetPasswordTokenAsync(email, phonenumber);

        public async Task<UserResponse> GeneratePhoneNumberChangeTokenAsync(ChangePhoneNumberDto phoneNumber) =>
            await _accountRepository.GeneratePhoneNumberChangeTokenAsync(phoneNumber);
        
    }
}
