using HumanResources.Application.Authentication;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Repository;
using HumanResources.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;

namespace HumanResources.Infrastructure.Repository
{
    public class DepartmentRepository : IDepartmentReposiotry
    {
        private readonly HumanResourcesDatabase _database;

        private readonly UserManager<User> _userManager;

        private readonly IUserContext _userContext;

        public DepartmentRepository(HumanResourcesDatabase database, UserManager<User> userManager, IUserContext userContext)
        {
            _database = database;
            _userManager = userManager;
            _userContext = userContext;
        }
    }
}
