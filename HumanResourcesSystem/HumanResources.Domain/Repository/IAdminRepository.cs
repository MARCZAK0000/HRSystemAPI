using HumanResources.Domain.AdminModelDto;
using HumanResources.Domain.Enums;

namespace HumanResources.Domain.Repository
{
    public interface IAdminRepository
    {
        Task<GetUserDto> AddToRoleAsync(string userId, RolesEnum rolesEnum);

        Task<GetUserDto> RemoveRoleAsync(string userId, RolesEnum rolesEnum);
    }
}
