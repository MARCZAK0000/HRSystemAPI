using HumanResources.Application.Authentication;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Exceptions;
using HumanResources.Domain.Repository;
using HumanResources.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.Infrastructure.Repository
{
    public class AccountRepository:IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserContext _userContext;
        private readonly HumanResourcesDatabase _database;

        public AccountRepository(UserManager<User> userManager, IUserContext userContext, HumanResourcesDatabase database)
        {
            _userManager = userManager;
            _userContext = userContext;
            _database = database;
        }

        public async Task<UserInfo> GetInfomrationsAboutUserAsync(string email, string phonenumber)
        {
            var currentUser = _userContext.GetCurrentUser();

            var user = await _userManager.FindByIdAsync(currentUser.Id);

            if (user == null && currentUser.IsInRole("User"))
            {
                throw new InvalidEmailOrPasswordExcepiton("ChangePassword: Password and Confirm Password are not the same");
            }

            var result = await _database.UserInfo.FirstOrDefaultAsync(pr=>pr.Email == email);

            if (result == null)
            {
                throw new NotFoundException("Something went wrong with database");
            }

            return result;
        }
    }
}
