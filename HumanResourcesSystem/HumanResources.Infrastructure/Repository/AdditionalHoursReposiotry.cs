using HumanResources.Application.Authentication;
using HumanResources.Domain.AdditionalHoursDto;
using HumanResources.Domain.AdditionalHoursModel;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Enums;
using HumanResources.Domain.Exceptions;
using HumanResources.Domain.Pagination;
using HumanResources.Domain.Repository;
using HumanResources.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.Infrastructure.Repository
{
    public class AdditionalHoursReposiotry : IAdditionalHoursReposiotry
    {
        private readonly IUserContext _userContext;

        private readonly HumanResourcesDatabase _dbContext;

        private readonly UserManager<User> _userManager;

        public AdditionalHoursReposiotry(IUserContext userContext, HumanResourcesDatabase dbContext, UserManager<User> userManager)
        {
            _userContext = userContext;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<AdditionalHoursResponse> CreateAdditionalHoursRequestAsync(CreateAdditionalHoursRequestDto hours, CancellationToken token)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id)
                ?? throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            if (!await _userManager.IsInRoleAsync(user, nameof(RolesEnum.Manager))) 
            {
                throw new ForbidenException("You don't have permission for that request");
            }

            var findSuperVisior = await _dbContext
                .UserInfo
                .Include(pr=>pr.Department)
                .Where(pr => pr.UserId == user.Id && pr.IsSupervisior == true)
                .FirstOrDefaultAsync(token)??
                throw new NotFoundException("There is a problem with supervisior database, contact with admin");

            var findUser = await
                _dbContext
                .UserInfo
                .Where(pr => pr.UserCode == hours.UserCode && pr.DepartmentID == findSuperVisior.DepartmentID)
                .Select(pr=>pr.UserId)
                .FirstOrDefaultAsync(cancellationToken: token) ??
                throw new NotFoundException("Invalid UserCode or you don't have that user in your deparment");

            if(await _dbContext.AdditionalHours.AnyAsync
                (pr=>pr.UserCode == hours.UserCode && pr.CreatedDay.Date == DateTime.Now.Date, token))
            {
                throw new BadRequestException("There is an additional hours reguest for this day");
            }

            var createRequest = new AdditionalHours()
            {
                Title = hours.Title,
                UserCode = hours.UserCode,
                UserID = findUser,
                SuperVisiorID = user.Id,
                CreatedDay = DateTime.Now,
                StartTime = hours.StartTime,
                EndTime = hours.EndTime,
            };
            createRequest.CalculateAdditionalHours();

            await _dbContext.AdditionalHours.AddAsync(createRequest, token);
            await _dbContext.SaveChangesAsync(token);

            return new AdditionalHoursResponse
            {
                Title = createRequest.Title,
                UserCode = createRequest.UserCode,
                StartTime = createRequest.StartTime,
                EndTime = createRequest.EndTime,
                Duration = createRequest.Duration,
            };


        }


        public async Task<AdditionalHoursResponse> UpdateAdditionalHoursRequestAsync(UpdateAdditionalHoursRequestDto hours, CancellationToken token)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id)
                ?? throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            if (!await _userManager.IsInRoleAsync(user, nameof(RolesEnum.Manager)))
            {
                throw new ForbidenException("You don't have permission for that request");
            }

            var findSuperVisior = await _dbContext
                .UserInfo
                .Include(pr => pr.Department)
                .Where(pr => pr.UserId == user.Id && pr.IsSupervisior == true)
                .FirstOrDefaultAsync(token) ??
                throw new NotFoundException("There is a problem with supervisior database, contact with admin");

            var findRequest = await
                _dbContext
                .AdditionalHours
                .Include(pr=>pr.User)
                .Where(pr => pr.UserCode == hours.UserCode && 
                    pr.User.DepartmentID == findSuperVisior.DepartmentID && 
                        pr.AdditionalHoursID == hours.AdditionalHoursID)
                .FirstOrDefaultAsync(cancellationToken: token) ??
                throw new NotFoundException("Invalid UserCode or you don't have that user in your deparment");


            findRequest.Title = hours.Title;
            findRequest.CreatedDay = DateTime.Now;
            findRequest.StartTime = hours.StartTime;
            findRequest.EndTime = hours.EndTime;
            findRequest.CalculateAdditionalHours();

            await _dbContext.SaveChangesAsync(token);

            return new AdditionalHoursResponse()
            {
                Title = findRequest.Title,
                UserCode = findRequest.UserCode,
                StartTime = findRequest.StartTime,
                EndTime = findRequest.EndTime,
                Duration = findRequest.Duration,
            };
               
        }

        public async Task<AdditionalHoursResponse> DeleteAdditionalHoursRequestAsync(DeleteAdditionalHoursRequestDto hours, CancellationToken token)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id)
                ?? throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            if (!await _userManager.IsInRoleAsync(user, nameof(RolesEnum.Manager)))
            {
                throw new ForbidenException("You don't have permission for that request");
            }

            var findRequest = await _dbContext
                .AdditionalHours
                .Where(pr => pr.AdditionalHoursID == hours.AdditionalHoursID && pr.UserCode == hours.UserCode)
                .FirstOrDefaultAsync(token) ??
                throw new NotFoundException("Invalid ID or UserCode");

            _dbContext.AdditionalHours.Remove(findRequest);

            await _dbContext.SaveChangesAsync(token);

            return new AdditionalHoursResponse()
            {
                Title = findRequest.Title,
                UserCode = findRequest.UserCode,
                StartTime = findRequest.StartTime,
                EndTime = findRequest.EndTime,
                Duration = findRequest.Duration,
            };
    }

        public async Task<PaginationBase<List<AdditionalHoursResponse>>> ShowAllAdditionalHoursRequestAsync(ShowAllAdditionalHoursDto hours, CancellationToken token)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id)
                ?? throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            if (!await _userManager.IsInRoleAsync(user, nameof(RolesEnum.Manager)))
            {
                hours.UserCode = currentUser.UserCode;
            }

            var response = new PaginationBuilder<List<AdditionalHoursResponse>>();   

            var baseRequestList =
                _dbContext.
                AdditionalHours
                .Where(pr => pr.UserCode == hours.UserCode)
                .Select(pr => new AdditionalHoursResponse
                {
                    Title = pr.Title,
                    UserCode = pr.UserCode,
                    StartTime = pr.StartTime,
                    EndTime = pr.EndTime,
                    Duration = pr.Duration,
                });

            var count = await baseRequestList.CountAsync(token);

            var requestList = await baseRequestList
                .Skip(hours.PageSize*(hours.PageNumber-1))
                .Take(hours.PageSize)
                .ToListAsync(token);

            return response.
                SetItems(items: requestList).
                SetTotalCount(TotalItemCount: count).
                SetItemsFrom(pageSize: hours.PageSize, pageNumber: hours.PageNumber).
                SetPageParameters(count, hours.PageSize, hours.PageNumber).
                Build();
                
        }

        public async Task<PaginationBase<List<AdditionalHoursResponse>>> ShowAdditionalHoursRequestByDateAsync(ShowAdditionalHoursDateDto hours, CancellationToken token)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id)
                ?? throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            if (!await _userManager.IsInRoleAsync(user, nameof(RolesEnum.Manager)))
            {
                hours.UserCode = currentUser.UserCode;
            }

            var response = new PaginationBuilder<List<AdditionalHoursResponse>>();


            var resultBase = _dbContext.AdditionalHours
                .Where(pr => pr.UserCode == hours.UserCode &&
                    pr.StartTime >= hours.From &&
                        pr.StartTime < hours.To)
                .Select(pr => new AdditionalHoursResponse
                {
                    Title = pr.Title,
                    UserCode = pr.UserCode,
                    StartTime = pr.StartTime,
                    EndTime = pr.EndTime,
                    Duration = pr.Duration,
                });

            var count = await resultBase.CountAsync(token);

            var result = await resultBase
                    .Skip(hours.PageSize*(hours.PageNumber-1))
                    .Take(hours.PageSize)
                    .ToListAsync(token);

            return response.
                SetItems(items: result).
                SetTotalCount(TotalItemCount: count).
                SetItemsFrom(pageSize: hours.PageSize, pageNumber: hours.PageNumber).
                SetPageParameters(count, hours.PageSize, hours.PageNumber).
                Build();

        }

        public async Task<AdditionalHoursResponse> ShowAdditionalHoursRequestByIDAsync(ShowAdditionalHoursDto hours, CancellationToken token)
        {

            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id)
                ?? throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            if (!await _userManager.IsInRoleAsync(user, nameof(RolesEnum.Manager)))
            {
                hours.UserCode = currentUser.UserCode;
            }

            var result = await _dbContext
                .AdditionalHours
                .Where(pr => pr.UserCode == hours.UserCode && pr.AdditionalHoursID == hours.AdditionalHoursID)
                .Select(pr => new AdditionalHoursResponse
                {
                    Title = pr.Title,
                    UserCode = pr.UserCode,
                    StartTime = pr.StartTime,
                    EndTime = pr.EndTime,
                    Duration = pr.Duration
                })
                .FirstOrDefaultAsync(token)
                ?? throw new NotFoundException("Invalid ID or UserCode");

            return result!;
        }

    }
}
