using HumanResources.Domain.ModelDtos;
using HumanResources.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Application.CQRS.IUserCommand
{
    public interface IUserCommandService
    {
        Task<UserResponse> RegisterUserAsync(RegisterUserAsyncDto register);

        Task<UserResponse> SignInUserAsync(LoginUserAsyncDto loginUser);

        Task<UserResponse> ConfirmEmailAsync(string email, string token);

        Task<UserResponse> ChangePasswordAsync(ChangePasswordAsyncDto changePassword);

        Task<UserResponse> ForgetPasswordAsync(ForgetPasswordNewPasswordAsyncDto forgetPassword);
    }
}
