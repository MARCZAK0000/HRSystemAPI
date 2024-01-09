using HumanResources.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Application.CQRS.IUserHandler
{
    public interface IUserHandlerService
    {
        Task<UserInfoResponse> GetInforamtionsAboutUserAsync(string email, string phonenumer);

        Task<UserResponse> GenerateConfirmEmailTokenAsync(string email);

        Task<UserResponse> GenerateForgetPasswordTokenAsync(string email, string phonenumber);
    }
}
