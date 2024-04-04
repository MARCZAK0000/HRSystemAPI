using HumanResources.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.AdminModelDto
{
    public class GetUserDto
    {
        public GetUserDto(string email, string userName, string userCode, List<string> roles)
        {
            Email = email;
            UserName = userName;
            UserCode = userCode;
            Roles = roles;
        }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string UserCode { get; set; } 

        public List<string> Roles { get; set; }


    }
}
