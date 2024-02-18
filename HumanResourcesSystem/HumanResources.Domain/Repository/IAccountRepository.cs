using HumanResources.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.Repository
{
    public interface IAccountRepository
    {
        Task<UserInfo> GetInfomrationsAboutUserAsync(string email, string phonenumber);
    }
}
