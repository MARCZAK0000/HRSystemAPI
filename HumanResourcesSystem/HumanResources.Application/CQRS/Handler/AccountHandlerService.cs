using AutoMapper;
using HumanResources.Application.CQRS.IUserHandler;
using HumanResources.Domain.ModelDtos;
using HumanResources.Domain.Repository;
using HumanResources.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Application.CQRS.UserHandler
{
    public class AccountHandlerService : IAccountHandlerService
    {

        private readonly IAccountReposiotry _accountRepository;

        public AccountHandlerService(IAccountReposiotry accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<UserResponse> GenerateConfirmEmailTokenAsync(string email) =>
            await _accountRepository.GenerateConfirmEmailTokenAsync(email);

        public async Task<UserResponse> GenerateForgetPasswordTokenAsync(string email, string phonenumber) =>
            await _accountRepository.GenerateForgetPasswordTokenAsync(email, phonenumber);

        public async Task<UserResponse> GeneratePhoneNumberChangeTokenAsync(ChangePhoneNumberDto phoneNumber) =>
            await _accountRepository.GeneratePhoneNumberChangeTokenAsync(phoneNumber);
        
    }
}
