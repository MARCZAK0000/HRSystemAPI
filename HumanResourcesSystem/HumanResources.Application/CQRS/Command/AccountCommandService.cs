using HumanResources.Application.CQRS.IUserCommand;
using HumanResources.Domain.ModelDtos;
using HumanResources.Domain.Repository;
using HumanResources.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Application.CQRS.UserCommand
{
    public class AccountCommandService : IAccountCommandService
    {
        private readonly IAccountReposiotry _accountRepository;

        public AccountCommandService(IAccountReposiotry accountReposiotry)
        {
            _accountRepository = accountReposiotry;
        }

        public async Task<UserResponse> RegisterUserAsync(RegisterAccountAsyncDto register) => 
            await _accountRepository.RegisterAsync(register);

        public async Task<UserResponse> SignInUserAsync(LoginAccountAsyncDto loginUser) => 
            await _accountRepository.SignInAccountAsync(loginUser);

        public async Task<UserResponse> ConfirmEmailAsync(string email, string token) => 
            await _accountRepository.ConfirmEmailAsync(email, token);
      
        public async Task<UserResponse> ChangePasswordAsync(ChangePasswordAsyncDto changePassword) => 
            await _accountRepository.ChangePasswordAsync(changePassword);

        public async Task<UserResponse> ForgetPasswordAsync(ResetPasswordAsyncDto forgetPassword) => 
            await _accountRepository.ForgetPasswordAsync(forgetPassword);

        public async Task<bool> UpdateUserInfromationsAsync(UpdateAccountInformationsDto updateAccountInformations) =>
            await _accountRepository.UpdateAccountInfromationsAsync(updateAccountInformations);
        
    }
}
