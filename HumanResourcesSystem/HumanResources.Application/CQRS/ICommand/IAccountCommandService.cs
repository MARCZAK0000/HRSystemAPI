using HumanResources.Domain.ModelDtos;
using HumanResources.Domain.Repository;
using HumanResources.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Application.CQRS.IUserCommand
{
    public interface IAccountCommandService
    {
        Task<UserResponse> RegisterUserAsync(RegisterAccountAsyncDto register);

        Task<UserResponse> SignInUserAsync(LoginAccountAsyncDto loginUser);

        Task<UserResponse> RefreshTokenAsync(string refreshToken);

        Task<UserResponse> ConfirmEmailAsync(string email, string token);

        Task<UserResponse> ChangePasswordAsync(ChangePasswordAsyncDto changePassword);

        Task<UserResponse> ForgetPasswordAsync(ResetPasswordAsyncDto forgetPassword);
    }
}
