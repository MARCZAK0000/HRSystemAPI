using HumanResources.Domain.AdminModelDto;
using HumanResources.Domain.Enums;

namespace HumanResources.Domain.Repository
{
    public interface IAdminRepository
    {
        Task<GetUserDto> AddToLeaderAsync(string userCode, RolesEnum role, CancellationToken token);
        Task<GetUserDto> AddToManagerAsync(string userCode, RolesEnum role, CancellationToken token);
        Task<GetUserDto> AddToAdminAsync(string userCode, RolesEnum role, CancellationToken token);

        Task<GetUserDto> RemoveLeaderAsync(string userCode, RolesEnum role, CancellationToken token);
        Task<GetUserDto> RemoveManagerAsync(string userCode, RolesEnum role, CancellationToken token);
        Task<GetUserDto> RemoveAdminAsync(string userCode, RolesEnum role, CancellationToken token);
    }
}
