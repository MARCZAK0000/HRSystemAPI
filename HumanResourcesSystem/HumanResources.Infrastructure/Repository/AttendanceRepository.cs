using AutoMapper;
using HumanResources.Application.Authentication;
using HumanResources.Domain.AttendanceModelDto;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Enums;
using HumanResources.Domain.Events;
using HumanResources.Domain.Exceptions;
using HumanResources.Domain.Repository;
using HumanResources.Domain.UserModelDto;
using HumanResources.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.Infrastructure.Repository
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly IUserContext _userContext;
        private readonly HumanResourcesDatabase _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public AttendanceRepository(IUserContext userContext, 
            HumanResourcesDatabase dbContext,
            UserManager<User> userManager,
            IMapper mapper)
        {
            _userContext = userContext;
            _dbContext = dbContext;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<bool> UserArrivalAsync(UserArrivalDto userArrival)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            var checkRequest = await _dbContext
                .Arrivals
                .FirstOrDefaultAsync(pr => pr.UserId == user.Id 
                    && pr.CreateDay.Date == userArrival.ArrivalDate.Date);

            if (checkRequest is not null)
            {
                throw new BadRequestException("You already create arrival request at this day");
            }

            var newArrival = new Arrivals()
            {
                UserId = user.Id,
                UserCode = user.UserCode,
                Arrival = userArrival.ArrivalDate,
                CreateDay = DateTime.Now.Date
                
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
                    pr.CreateDay.Year == year && 
                        pr.CreateDay.Month == month )
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
                .FirstOrDefaultAsync(pr => pr.CreateDay.Date == date.Date)
                ?? throw new NotFoundException($"We cannot find arrival with that date: {date.Date}");


            return result;
        }

        public async Task<GetAttendanceStatsDto> GetUserAttendanceStatsByMontAsync(GetAttendanceByMonthDto monthDto)
        {
            var currentUser = _userContext.GetCurrentUser();

            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            var year = monthDto.Year;
            var month = Convert.ToInt32(monthDto.Month);


            var baseQuery = _dbContext.Arrivals
                .Where(pr => pr.UserId == user.Id
                    && pr.CreateDay.Year == year
                        && pr.CreateDay.Month == month) ?? 
                        throw new NotFoundException("There is no attendance in this month");


            var completedDays = await baseQuery
                .CountAsync(pr => pr.IsCompleted == true);

            var notCompletedDays = await baseQuery
                .CountAsync(pr => pr.IsCompleted == false);

            var numberOfDays = await baseQuery
                .CountAsync();

            var listOfCompletedDays = _mapper.Map<List<GetArrivalsDto>>(await baseQuery
                .Where(pr => pr.IsCompleted == true)
                .ToListAsync());

            return new GetAttendanceStatsDto(completedDays, notCompletedDays, numberOfDays, listOfCompletedDays);

        }

        public async Task<List<Arrivals>> GetUserCompletedOrNotAttendenceByMonthAsync(GetAttendanceByMonthDto monthDto, bool isCompleted)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            var month = Convert.ToInt32(monthDto.Month);
            var year = monthDto.Year;

            var baseQuery = _dbContext.Arrivals
                .Where(pr => pr.UserId == user.Id && pr.CreateDay.Year == year && pr.CreateDay.Month == month);

            if(!baseQuery.Any()) 
            {
                throw new BadRequestException("Invalid date");
            }

            var result = await baseQuery.
                Where(pr => pr.IsCompleted == isCompleted)
                .ToListAsync();

            return result;
        }

        public async Task<GetAttendanceStatsDto> GetInformationsAboutUserForLeadersAttendanceByMonth(GetAttendanceByMonthDto monthDto, string userCode)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id)??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            var isRole = await _userManager.IsInRoleAsync(user, nameof(RolesEnum.Leader));
            if (!isRole)
            {
                throw new UnauthorizedExceptions("UnAuthorize");
            }

            var year = monthDto.Year;
            var month = Convert.ToInt32(monthDto.Month);

            var baseQuery = _dbContext.Arrivals
                .Where(pr => pr.UserCode == userCode && pr.CreateDay.Year == year && pr.CreateDay.Month == month);

            var completedDays = await baseQuery.CountAsync(pr=>pr.IsCompleted == true);

            var notCompletedDays = await baseQuery.CountAsync(pr => pr.IsCompleted == false);

            var countDays = await baseQuery.CountAsync();

            var listOfCompletedDays = _mapper.Map<List<GetArrivalsDto>>(await baseQuery.Where(pr=>pr.IsCompleted == true).ToListAsync());

            return new GetAttendanceStatsDto(completedDays, notCompletedDays, countDays, listOfCompletedDays);
        }
    }
}
