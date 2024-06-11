using HumanResources.Domain.EmailModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.Repository
{
    public interface IHelperRepository
    {
        Task<string> GenerateRandomUserCode();

        string ConfirmEmailBody(ConfirmEmailMessageInfoDto confirmEmail);

        string GenerateForgetPasswordToken(ConfirmEmailMessageInfoDto generateToken);

        string GenerateRandomKey();
    }
}
