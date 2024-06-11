using Azure.Data.Tables;
using HumanResources.Application.Authentication;
using HumanResources.Domain.AdminModelDto;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Enums;
using HumanResources.Domain.Exceptions;
using HumanResources.Domain.Repository;
using HumanResources.Domain.StorageAccountModel;
using HumanResources.Infrastructure.Database;
using HumanResources.Infrastructure.StorageAccount;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.Infrastructure.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly UserManager<User> _userManager;

        private readonly HumanResourcesDatabase _dbContex;

        private readonly IUserContext _userContext;

        private readonly StorageAccountSettings _storageAccountSettings;

        private readonly TableServiceClient _tableServiceClient;

        private readonly IHelperRepository _helperRepository;

        public AdminRepository(UserManager<User> userManager,
            HumanResourcesDatabase dbContex,
            IUserContext userContext,
            StorageAccountSettings storageAccountSettings,
            TableServiceClient tableServiceClient,
            IHelperRepository helperRepository)
        {
            _userManager = userManager;
            _dbContex = dbContex;
            _userContext = userContext;
            _storageAccountSettings = storageAccountSettings;
            _tableServiceClient = tableServiceClient;
            _helperRepository = helperRepository;
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

            if (findUser.IsSupervisior)
            {
                return new GetUserDto(findUser.User.Email!, findUser.User.UserName!, findUser.UserCode, userRole.ToList());
            }

            findUser.IsSupervisior = true;

            await _dbContex.SaveChangesAsync(token);
            return new GetUserDto(findUser.User.Email!, findUser.User.UserName!, findUser.UserCode, userRole.ToList());
        }

        public async Task<bool> PublishAdminApiKey(string userCode, CancellationToken token)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            var checkRole = await _userManager.IsInRoleAsync(user, nameof(RolesEnum.Admin));

            if (!checkRole)
            {
                throw new ForbidenException("Forbidden");
            }

            var findUser = await _dbContex.UserInfo
              .Include(pr => pr.User)
              .Where(pr => pr.UserCode == userCode)
              .FirstOrDefaultAsync(cancellationToken: token) ??
              throw new NotFoundException("Invalid UserCode");

            var table = _tableServiceClient.GetTableClient(_storageAccountSettings.TableContinerName);
            await table.AddEntityAsync(new AdminTableEntity
            {
                PartitionKey = findUser.UserId,
                RowKey = findUser.UserId,
                UserID = findUser.UserId,
                Email = findUser.User.Email!,
                UserCode = findUser.UserCode,
                Key = _helperRepository.GenerateRandomKey()
            }, token);

            return true;
        }

        public async Task<GetUserDto> AddToAdminAsync(string userCode, string key, CancellationToken token)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            var checkRole = await _userManager.IsInRoleAsync(user, nameof(RolesEnum.Admin));

            if (!checkRole)
            {
                throw new ForbidenException("Forbidden");
            }

            var findUser = await _dbContex.UserInfo
              .Include(pr => pr.User)
              .Where(pr => pr.UserCode == userCode)
              .FirstOrDefaultAsync(cancellationToken: token) ??
              throw new NotFoundException("Invalid UserCode");

            var rolename = nameof(RolesEnum.Admin);

            var checkuser = await _userManager.IsInRoleAsync(findUser.User, rolename);

            if (checkuser)
            {
                throw new BadRequestException("user is in role already");
            }

            var adminContainer = _tableServiceClient.GetTableClient(_storageAccountSettings.TableContinerName);

            var result = adminContainer.Query<AdminTableEntity>(pr => pr.Key == key && pr.UserID == findUser.UserId);

            if (!result.Any())
            {
                throw new BadRequestException("Invalid AdminKey");
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

            await _userManager.RemoveFromRoleAsync(findUser.User, roleName);
            findUser.IsSupervisior = false;

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
