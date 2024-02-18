using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Application.Authentication
{
    public class CurrentUser
    {
        public CurrentUser(string id, IEnumerable<string> roles, string userCode)
        {
            Id = id;
            Roles = roles;
            UserCode = userCode;
        }

        public string Id { get; private set; }

        private IEnumerable<string> Roles { get; set;}

        public string UserCode { get; set; }

        public bool IsInRole(string RoleName)=> Roles.Contains(RoleName);
    }
}
