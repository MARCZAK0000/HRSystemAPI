using HumanResources.Application.Authentication;
using HumanResources.Domain.AdminModelDto;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Enums;
using HumanResources.Domain.Exceptions;
using HumanResources.Domain.Repository;
using HumanResources.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
        public async Task<GetUserDto> AddToLeaderAsync(string userCode, RolesEnum role, CancellationToken token)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            var checkRole = await _userManager.IsInRoleAsync(user, nameof(RolesEnum.Admin));

            if (!checkRole)
            {
                throw new ForbidenException("UnAuthorize");
            }

            var findUser = await _dbContex.UserInfo
               .Include(pr => pr.User)
               .Where(pr => pr.UserCode == userCode)
               .FirstOrDefaultAsync(cancellationToken: token) ??
               throw new NotFoundException("Invalid UserCode");

            var rolename = role.ToString();

            var checkuser = await _userManager.IsInRoleAsync(findUser.User, rolename);

            if (checkuser)
            {
                throw new BadRequestException("user is in role already");
            }

            await _userManager.AddToRoleAsync(findUser.User, rolename);

            var userRole = await _userManager.GetRolesAsync(findUser.User);

            return new GetUserDto(findUser.User.Email!, findUser.User.UserName!, findUser.UserCode, userRole.ToList());

        }

        public async Task<GetUserDto> AddToManagerAsync(string userCode, RolesEnum role, CancellationToken token)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            var checkRole = await _userManager.IsInRoleAsync(user, nameof(RolesEnum.Admin));

            if (!checkRole)
            {
                throw new ForbidenException("UnAuthorize");
            }

            var findUser = await _dbContex.UserInfo
              .Include(pr => pr.User)
              .Where(pr => pr.UserCode == userCode)
              .FirstOrDefaultAsync(cancellationToken: token) ??
              throw new NotFoundException("Invalid UserCode");

            var rolename = role.ToString();

            var checkuser = await _userManager.IsInRoleAsync(findUser.User, rolename);

            if (checkuser)
            {
                throw new BadRequestException("user is in role already");
            }

            await _userManager.AddToRoleAsync(findUser.User, rolename);

            var userRole = await _userManager.GetRolesAsync(findUser.User);

            if(await _dbContex.Supervisiors.AnyAsync(pr => pr.UserID == findUser.UserId, token)) 
            {
                return new GetUserDto(findUser.User.Email!, findUser.User.UserName!, findUser.UserCode, userRole.ToList());
            }
            var superVisior = new Supervisiors()
            {
                UserID = findUser.UserId,
                DepramentID = findUser.DepartmentID
            };

            await _dbContex.Supervisiors.AddAsync(superVisior, token);
            await _dbContex.SaveChangesAsync(token);

            return new GetUserDto(findUser.User.Email!, findUser.User.UserName!, findUser.UserCode, userRole.ToList());
        }

        public async Task<GetUserDto> AddToAdminAsync(string userCode, RolesEnum role, CancellationToken token)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            var checkRole = await _userManager.IsInRoleAsync(user, nameof(RolesEnum.Admin));

            if (!checkRole)
            {
                throw new ForbidenException("UnAuthorize");
            }

            var findUser = await _dbContex.UserInfo
              .Include(pr => pr.User)
              .Where(pr => pr.UserCode == userCode)
              .FirstOrDefaultAsync(cancellationToken: token) ??
              throw new NotFoundException("Invalid UserCode");

            var rolename = role.ToString();

            var checkuser = await _userManager.IsInRoleAsync(findUser.User, rolename);

            if (checkuser)
            {
                throw new BadRequestException("user is in role already");
            }

            await _userManager.AddToRoleAsync(findUser.User, rolename);

            var userRole = await _userManager.GetRolesAsync(findUser.User);

            return new GetUserDto(findUser.User.Email!, findUser.User.UserName!, findUser.UserCode, userRole.ToList());
        }


        public async Task<GetUserDto> RemoveLeaderAsync(string userCode, RolesEnum role, CancellationToken token)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            var checkRole = await _userManager.IsInRoleAsync(user, nameof(RolesEnum.Admin));

            if (!checkRole)
            {
                throw new ForbidenException("UnAuthorize");
            }
            var findUser = await _dbContex.UserInfo
                .Include(pr => pr.User)
                .Where(pr => pr.UserCode == userCode)
                .FirstOrDefaultAsync(cancellationToken: token) ??
                throw new NotFoundException("Invalid UserCode");

            var roleName = role.ToString();

            var checkUser = await _userManager.IsInRoleAsync(findUser.User, roleName);

            if (!checkUser)
            {
                throw new BadRequestException("User isn't in role");
            }

            await _userManager.RemoveFromRoleAsync(findUser.User, roleName);


            var userRole = await _userManager.GetRolesAsync(findUser.User);

            return new GetUserDto(findUser.User.Email!, findUser.User.UserName!, findUser.UserCode, userRole.ToList());
        }

        public async Task<GetUserDto> RemoveManagerAsync(string userCode, RolesEnum role, CancellationToken token)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            var checkRole = await _userManager.IsInRoleAsync(user, nameof(RolesEnum.Admin));

            if (!checkRole)
            {
                throw new ForbidenException("UnAuthorize");
            }

            var findUser = await _dbContex.UserInfo
                 .Include(pr => pr.User)
                 .Where(pr => pr.UserCode == userCode)
                 .FirstOrDefaultAsync(cancellationToken: token) ??
                 throw new NotFoundException("Invalid UserCode");

            var roleName = role.ToString();

            var checkUser = await _userManager.IsInRoleAsync(findUser.User, roleName);

            if (!checkUser)
            {
                throw new BadRequestException("User isn't in role");
            }

            var findManager = await _dbContex.
                Supervisiors
                .Where(pr=>pr.UserID == findUser.UserId)
                .FirstOrDefaultAsync(token)??
                throw new NotFoundException("Not found Manager");

            await _userManager.RemoveFromRoleAsync(findUser.User, roleName);


            _dbContex.Supervisiors.Remove(findManager);


            await _dbContex.SaveChangesAsync(token);
            var userRole = await _userManager.GetRolesAsync(findUser.User);

            return new GetUserDto(findUser.User.Email!, findUser.User.UserName!, findUser.UserCode, userRole.ToList());

        }

        public async Task<GetUserDto> RemoveAdminAsync(string userCode, RolesEnum role, CancellationToken token)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            var checkRole = await _userManager.IsInRoleAsync(user, nameof(RolesEnum.Admin));

            if (!checkRole)
            {
                throw new ForbidenException("UnAuthorize");
            }

            var findUser = await _dbContex.UserInfo
                  .Include(pr => pr.User)
                  .Where(pr => pr.UserCode == userCode)
                  .FirstOrDefaultAsync(cancellationToken: token) ??
                  throw new NotFoundException("Invalid UserCode");

            var roleName = role.ToString();

            var checkUser = await _userManager.IsInRoleAsync(findUser.User, roleName);

            if (!checkUser)
            {
                throw new BadRequestException("User isn't in role");
            }

            await _userManager.RemoveFromRoleAsync(findUser.User, roleName);


            var userRole = await _userManager.GetRolesAsync(findUser.User);

            return new GetUserDto(findUser.User.Email!, findUser.User.UserName!, findUser.UserCode, userRole.ToList());

        }




    }
}
