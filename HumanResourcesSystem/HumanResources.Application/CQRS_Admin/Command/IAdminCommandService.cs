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
        Task<GetUserDto> AddToLeaderAsync(string userCode, RolesEnum role, CancellationToken token);
        Task<GetUserDto> AddToManagerAsync(string userCode, RolesEnum role, CancellationToken token);
        Task<GetUserDto> AddToAdminAsync(string userCode, RolesEnum role, CancellationToken token);

        Task<GetUserDto> RemoveLeaderAsync(string userCode, RolesEnum role, CancellationToken token);
        Task<GetUserDto> RemoveManagerAsync(string userCode, RolesEnum role, CancellationToken token);
        Task<GetUserDto> RemoveAdminAsync(string userCode, RolesEnum role, CancellationToken token);
    }
}
