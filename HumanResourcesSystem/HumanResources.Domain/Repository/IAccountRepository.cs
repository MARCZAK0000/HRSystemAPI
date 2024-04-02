using HumanResources.Domain.Entities;
using HumanResources.Domain.ModelDtos;
using HumanResources.Domain.Response;

namespace HumanResources.Domain.Repository
{
    public interface IAccountReposiotry
    {
        Task<UserResponse> RegisterAsync(RegisterAccountAsyncDto registerUser);

        Task<UserResponse> GenerateConfirmEmailTokenAsync(string email);

        Task<UserResponse> ConfirmEmailAsync(string email, string token);

        Task<string> GenerateTokenAsync(User user);

        Task<UserResponse> SignInAccountAsync(LoginAccountAsyncDto loginUser);

        Task<UserResponse> ChangePasswordAsync(ChangePasswordAsyncDto changePassword);

        Task<UserResponse> ForgetPasswordAsync(ResetPasswordAsyncDto forgetPassword);

        Task<UserResponse> GenerateForgetPasswordTokenAsync(string email, string phonenumber);

        Task<UserResponse> GeneratePhoneNumberChangeTokenAsync(ChangePhoneNumberDto changePhoneNumber);



    }
}
