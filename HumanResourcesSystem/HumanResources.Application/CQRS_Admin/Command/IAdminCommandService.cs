using HumanResources.Domain.AdminModelDto;
using HumanResources.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Application.CQRS_Admin.Command
{
    public interface IAdminCommandService
    {
        Task<GetUserDto> AddToRoleAsync(string userId, RolesEnum role);

        Task<GetUserDto> RemoveRoleAsync(string userId, RolesEnum role);
    }
}
