using AutoMapper;
using HumanResources.Application.Authentication;
using HumanResources.Domain.AdminModelDto;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Enums;
using HumanResources.Domain.Exceptions;
using HumanResources.Domain.Repository;
using HumanResources.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;

namespace HumanResources.Infrastructure.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly UserManager<User> _userManager;

        private readonly HumanResourcesDatabase _dbContex;

        private readonly IUserContext _userContext;

        public AdminRepository(UserManager<User> userManager, HumanResourcesDatabase dbContex, IUserContext userContext)
        {
            _userManager = userManager;
            _dbContex = dbContex;
            _userContext = userContext;
        }

        public async Task<GetUserDto> AddToRoleAsync(string userId, RolesEnum rolesEnum)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            var checkRole = await _userManager.IsInRoleAsync(user, nameof(RolesEnum.Admin));

            if (!checkRole)
            {
                throw new ForbidenException("UnAuthorize");
            }

            var findUser = await _userManager.FindByIdAsync(userId)??
                throw new NotFoundException("Invalid UserId");


            var roleName = rolesEnum.ToString();

            var checkUser = await _userManager.IsInRoleAsync(findUser, roleName);

            if (checkUser)
            {
                throw new BadRequestException("User is in role already");    
            }

            await _userManager.AddToRoleAsync(findUser, roleName);

            var userRole = await _userManager.GetRolesAsync(findUser);

            return new GetUserDto(findUser.Email!, findUser.UserName!, findUser.UserCode, userRole.ToList());
        }

        public async Task<GetUserDto> RemoveRoleAsync(string userId, RolesEnum rolesEnum)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            var checkRole = await _userManager.IsInRoleAsync(user, nameof(RolesEnum.Admin));

            if(!checkRole) 
            {
                throw new ForbidenException("UnAuthorize");
            }

            var findUser = await _userManager.FindByIdAsync(userId) ??
                throw new NotFoundException("Invalid UserId");

            var roleName = rolesEnum.ToString();

            var checkUser = await _userManager.IsInRoleAsync(findUser, roleName);

            if (!checkUser)
            {
                throw new BadRequestException("User isn't in role");
            }

            await _userManager.RemoveFromRoleAsync(findUser, roleName);
            

            var userRole = await _userManager.GetRolesAsync(findUser);

            return new GetUserDto(findUser.Email, findUser.UserName, findUser.UserCode, userRole.ToList());
        }
    }
}
