using HumanResources.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace HumanResources.Domain.Entities
{
    public class User : IdentityUser
    {
        public string UserCode { get; set; }

        public UserInfo UserInfo { get; set; }
    }
}
