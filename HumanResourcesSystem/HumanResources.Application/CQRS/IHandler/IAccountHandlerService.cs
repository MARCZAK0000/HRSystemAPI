using HumanResources.Domain.EmailModelDto;
using HumanResources.Domain.ModelDtos;
using HumanResources.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Application.CQRS.IUserHandler
{
    public interface IAccountHandlerService
    {
      
        Task<EmailResponseDto> GenerateConfirmEmailTokenAsync();

        Task<UserResponse> GenerateForgetPasswordTokenAsync(string email, string phonenumber);

        Task<UserResponse> GeneratePhoneNumberChangeTokenAsync(ChangePhoneNumberDto phoneNumber);

    }
}
