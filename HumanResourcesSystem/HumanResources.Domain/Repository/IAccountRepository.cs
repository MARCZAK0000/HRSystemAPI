using HumanResources.Domain.EmailModelDto;
using HumanResources.Domain.Entities;
using HumanResources.Domain.ModelDtos;
using HumanResources.Domain.Response;

namespace HumanResources.Domain.Repository
{
    public interface IAccountReposiotry
    {
        Task<UserResponse> RegisterAsync(RegisterAccountAsyncDto registerUser);

        Task<EmailResponseDto> GenerateConfirmEmailTokenAsync(CancellationToken cancellationToken);

        Task<string> GenerateRefreshTokenAsync();

        Task<UserResponse> SignInAccountAsync(LoginAccountAsyncDto loginUser);

        Task<UserResponse> RefreshTokenAsync(string refreshToken);

        Task<bool> LogOutAsync();

        Task<UserResponse> ConfirmEmailAsync(string email, string token);

        Task<string> GenerateTokenAsync(User user);

        Task<UserResponse> ChangePasswordAsync(ChangePasswordAsyncDto changePassword);

        Task<UserResponse> ForgetPasswordAsync(ResetPasswordAsyncDto forgetPassword);

        Task<UserResponse> GenerateForgetPasswordTokenAsync(string email, string phonenumber, CancellationToken token);

        Task<UserResponse> GeneratePhoneNumberChangeTokenAsync(ChangePhoneNumberDto changePhoneNumber);



    }
}
