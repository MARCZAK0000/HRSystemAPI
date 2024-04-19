﻿using HumanResources.Application.Authentication;
using HumanResources.Domain.AbsenceModelDto;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Enums;
using HumanResources.Domain.Exceptions;
using HumanResources.Domain.Repository;
using HumanResources.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MimeKit.Tnef;

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

            var findAbsences = await _database.Absences
                .Where(pr => pr.UserId == user.Id)
                .FirstOrDefaultAsync(
                pr => (pr.StartTime.Date <= createAbsence.StartTime.Date && pr.EndTime.Date >= createAbsence.StartTime.Date) &&
                (pr.StartTime.Date <= createAbsence.EndTime.Date && pr.EndTime > createAbsence.EndTime.Date));

            if (findAbsences != null)
            {
                throw new BadRequestException("You have absences on this period of time");
            }

            var newAbsences = new Absence()
            {
                UserId = user.Id,
                Name = createAbsence.Name,
                AbsenceId = createAbsence.AbsenceTypeId,
                CreatedTime = DateTime.Now.Date,
                StartTime = createAbsence.StartTime.Date,
                EndTime = createAbsence.EndTime.Date,
            };
            newAbsences.CalculatePeriodOfTime();

            await _database.Absences
                .AddAsync(newAbsences);

            await _database.SaveChangesAsync();

            return newAbsences;

        }

        public async Task<List<Absence>> ShowAbsencesByYearAsync(int year)
        {
            var currentUser = _userContext.GetCurrentUser();

            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            if (year < 2020 || year > 2050)
            {
                throw new BadRequestException("Invalid year");
            }

            var result = await _database.Absences
                .Include(pr => pr.AbsencesType)
                .Where(pr => pr.StartTime.Year == year && pr.UserId == user.Id)
                .ToListAsync();

            if (!result.Any())
            {
                throw new NotFoundException("Not found absences in this year");
            }

            return result;
        }

        public async Task<Absence> AbsenceDecisionAsync(AbsenceDecisionInfoDto infoDto)
        {
            var currentUser = _userContext.GetCurrentUser();

            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            if (!await _userManager.IsInRoleAsync(user, nameof(RolesEnum.Leader)))
            {
                throw new UnauthorizedExceptions("UnAuthorized");
            }

            var getLeaderDepartmentID = await _database.UserInfo
                .FirstOrDefaultAsync(pr=>pr.UserId == user.Id)??
                throw new NotFoundException("NotFound leader");

            var baseQuery = _database
                .Absences
                .Include(pr => pr.AbsencesType)
                .Include(pr => pr.User)
                .Where(pr => pr.User.DepartmentID == getLeaderDepartmentID.DepartmentID);

            if(!baseQuery.Any()) 
            {
                throw new NotFoundException("User is not in your Department");
            }

            var subordinateAbsence = await baseQuery 
                .FirstOrDefaultAsync(pr => pr.Id == infoDto.AbsenceId && pr.User.UserCode == infoDto.UserCode) ??
                throw new NotFoundException("Not Found Absences");

            if (subordinateAbsence.IsAccepted)
            {
                throw new BadRequestException("Absence was accepted or decision is negative");
            }

            if (!infoDto.Decision)
            {
                subordinateAbsence.Declined = true;
                await _database.SaveChangesAsync();
                return subordinateAbsence;
            }

            subordinateAbsence.IsAccepted = infoDto.Decision;
            subordinateAbsence.User.DaysOfAbsencesCurrentYear += subordinateAbsence.PeriodOfTime;
            var daysToUse = subordinateAbsence.CalculateDayToUse(() =>
            {
                int daysToUse = 0;

                switch (subordinateAbsence.AbsenceId)
                {
                    case 1:
                        daysToUse = subordinateAbsence.PeriodOfTime;
                        break;

                    default:
                        break;
                }

                return daysToUse;
            });

            if(subordinateAbsence.User.DaysOfAbsencesToUse -  daysToUse < 0 && daysToUse > 0) 
            {
                subordinateAbsence.Declined = true;

                await _database.SaveChangesAsync();
                throw new BadRequestException($"User: {subordinateAbsence.User.UserCode} used all of his absence days");
            }


            subordinateAbsence.User.DaysOfAbsencesToUse-= daysToUse;
            await _database.SaveChangesAsync();
            return subordinateAbsence;


        }
    }
}
