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
        public CurrentUser(string id, IEnumerable<string> roles)
        {
            Id = id;
            Roles = roles;
        }

        public string Id { get; private set; }

        private IEnumerable<string> Roles { get; set;}

        public bool IsInRole(string RoleName)=> Roles.Contains(RoleName);
    }
}
