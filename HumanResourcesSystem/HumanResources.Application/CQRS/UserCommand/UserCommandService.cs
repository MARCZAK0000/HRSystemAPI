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
    public class UserCommandService : IUserCommandService
    {
        private readonly IUserRepository _userRepository;

        public UserCommandService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse> RegisterUserAsync(RegisterUserAsyncDto register) => 
            await _userRepository.RegisterAsync(register);

        public async Task<UserResponse> SignInUserAsync(LoginUserAsyncDto loginUser) => 
            await _userRepository.SignInUserAsync(loginUser);

        public async Task<UserResponse> ConfirmEmailAsync(string email, string token) => 
            await _userRepository.ConfirmEmailAsync(email, token);
      
        public async Task<UserResponse> ChangePasswordAsync(ChangePasswordAsyncDto changePassword) => 
            await _userRepository.ChangePasswordAsync(changePassword);

        public async Task<UserResponse> ForgetPasswordAsync(ForgetPasswordNewPasswordAsyncDto forgetPassword) => 
            await _userRepository.ForgetPasswordAsync(forgetPassword);
        
    }
}
