using HumanResources.Application.Authentication;
using HumanResources.Domain.AbsenceModelDto;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Exceptions;
using HumanResources.Domain.Repository;
using HumanResources.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;

namespace HumanResources.Infrastructure.Repository
{
    public class AbsenceRepository : IAbsenceRepository
    {
        private readonly HumanResourcesDatabase _database;

        private readonly UserManager<User> _userManager;

        private readonly IUserContext _userContext;

        public AbsenceRepository(HumanResourcesDatabase database, 
            UserManager<User> userManager, IUserContext userContext)
        {
            _database = database;
            _userManager = userManager;
            _userContext = userContext;
        }

        public async Task<Absence> CreateAbsenceAsync(CreateAbsenceDto createAbsence)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            var newAbsences = new Absence()
            {
                UserId = user.Id,
                Name = createAbsence.Name,
                AbsenceId = createAbsence.AbsenceTypeId,
                CreatedTime = DateTime.Now,
                StartTime = createAbsence.StartTime,
                EndTime = createAbsence.EndTime,
            };
            newAbsences.CalculatePeriodOfTime();

            await _database.Absences
                .AddAsync(newAbsences);

            await _database.SaveChangesAsync();

            return newAbsences;

        }
    }
}
