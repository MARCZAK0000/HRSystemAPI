using HumanResources.Domain.Entities;
using HumanResources.Domain.ModelDtos;
using HumanResources.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.Repository
{
    public interface IUserRepository
    {
        Task<UserResponse> RegisterAsync(RegisterUserAsyncDto registerUser);

        Task<UserResponse> GenerateConfirmEmailTokenAsync(string email);

        Task<UserResponse> ConfirmEmailAsync(string email, string token);

        Task<string> GenerateTokenAsync(User user);

        Task<UserResponse> SignInUserAsync(LoginUserAsyncDto loginUser);

        Task<UserResponse> ChangePasswordAsync(ChangePasswordAsyncDto changePassword);

        Task<UserResponse> ForgetPasswordAsync(ForgetPasswordNewPasswordAsyncDto forgetPassword);

        Task<UserResponse> GenerateForgetPasswordTokenAsync(string email, string phonenumber);

        
    }
}
