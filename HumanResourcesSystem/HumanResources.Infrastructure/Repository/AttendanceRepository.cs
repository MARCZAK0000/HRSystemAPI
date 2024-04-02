using HumanResources.Application.Authentication;
using HumanResources.Domain.AttendanceModelDto;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Events;
using HumanResources.Domain.Exceptions;
using HumanResources.Domain.Repository;
using HumanResources.Domain.UserModelDto;
using HumanResources.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Infrastructure.Repository
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly IUserContext _userContext;
        private readonly HumanResourcesDatabase _dbContext;
        private readonly UserManager<User> _userManager;

        public AttendanceRepository(IUserContext userContext, 
            HumanResourcesDatabase dbContext,
            UserManager<User> userManager)
        {
            _userContext = userContext;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<bool> UserArrivalAsync(UserArrivalDto userArrival)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            var newArrival = new Arrivals()
            {
                UserId = user.Id,
                UserCode = user.UserCode,
                Arrival = userArrival.ArrivalDate
            };

            await _dbContext.Arrivals.AddAsync(newArrival);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UserDepartureAsync(UserDepartureDto userDeparture)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id)??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            var findResult = await _dbContext
                .Arrivals
                .FirstOrDefaultAsync(pr => pr.Id == userDeparture.Id && pr.UserCode == user.UserCode)??
                throw new BadRequestException("We cannot find Request with that ID and this UserCode");

            findResult.Departure = userDeparture.DepartureDate;
            findResult.CompleteDay();

            await _dbContext.SaveChangesAsync();
            _dbContext.SaveChangesFailed += DatabaseFailed.SaveChangesFailed;


            return true;


        }

        public async Task<List<Arrivals>> GetUserAttendanceByMonthAsync(GetAttendanceByMonthDto monthDto)
        {
            
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            var month = Convert.ToInt32(monthDto.Month);
            var year = monthDto.Year;
     

            var result = await _dbContext.Arrivals
                .Where(pr => pr.UserId == user.Id && 
                    pr.Arrival.Value.Year == year && 
                        pr.Arrival.Value.Month == month )
                .ToListAsync();



            return result;
        }



        public async Task<Arrivals> GetUserAttendanceByDateAsync(DateTime date)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            var result = await _dbContext.Arrivals
                .Where(pr => pr.UserId == user.Id)
                .FirstOrDefaultAsync(pr => pr.Arrival!.Value.Date == date.Date)
                ?? throw new NotFoundException($"We cannot find arrival with that date: {date.Date}");


            return result;
        }
    }
}
