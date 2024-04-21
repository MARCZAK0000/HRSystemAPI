using HumanResources.Application.Authentication;
using HumanResources.Domain.DepartmentModelDto;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Enums;
using HumanResources.Domain.Events;
using HumanResources.Domain.Exceptions;
using HumanResources.Domain.Repository;
using HumanResources.Domain.Response;
using HumanResources.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        public async Task<DepartmentResponse> ChangeUserDepartmentAsync(ChangeDepartmentDto changeDepartment)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id)??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            if(!await _userManager.IsInRoleAsync(user, nameof(RolesEnum.Manager)))
            {
                throw new ForbidenException("Forbiden Request");
            }

            var getUser = await _database.UserInfo
                .Include(pr => pr.Department)
                .FirstOrDefaultAsync(pr => pr.UserCode == changeDepartment.UserCode)??
                throw new NotFoundException($"We cannot found user with that UserCode: {changeDepartment.UserCode}");

            getUser.DepartmentID = changeDepartment.DepartmentId;

            await _database.SaveChangesAsync();
            _database.SaveChangesFailed += DatabaseFailed.SaveChangesFailed;
            return new DepartmentResponse()
            {
                DepartmentId = changeDepartment.DepartmentId,
                Result = true,
                Message = "Well done"
            };
        }

        public async Task<Departments> DepartmentInfoAsync(int departmentId)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            if (!await _userManager.IsInRoleAsync(user, nameof(RolesEnum.Leader)))
            {
                throw new ForbidenException("Forbiden Request");
            }


            var result = await _database.Departments
                .Include(pr => pr.Users)
                .FirstOrDefaultAsync(pr => pr.Id == departmentId)??
                throw new NotFoundException($"We cannot find department with DepartmentID: {departmentId}");


            return result;

        }

        public async Task<DepartmentResponse> AddDepartmentAsync(DepartmentUpdateDto departmentInfo)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            if (!await _userManager.IsInRoleAsync(user, nameof(RolesEnum.Admin)))
            {
                throw new ForbidenException("Forbiden Request");
            }

            var newDepartment = new Departments()
            {
                Name = departmentInfo.Name,
                Description = departmentInfo.Description,
            };

            await _database.Departments
                .AddAsync(newDepartment);

            await _database.SaveChangesAsync();
            _database.SaveChangesFailed += DatabaseFailed.SaveChangesFailed;


            return new DepartmentResponse()
            {
                Result = true,
                Message = "Well Done"
            };
        }

     
        public async Task<DepartmentResponse> UpdateDepartmentAsync(DepartmentUpdateDto changeDepartment, int depratmentID)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            if (!await _userManager.IsInRoleAsync(user, nameof(RolesEnum.Admin)))
            {
                throw new ForbidenException("Forbiden Request");
            }

            var getDeparmentsCount = await _database.Departments
                .CountAsync();

            if(depratmentID < 1 || depratmentID > getDeparmentsCount) 
            {
                throw new BadRequestException($"Invalid index of department: 1 - {getDeparmentsCount}");
            }

            var getDepartment = await _database.
                Departments.
                FirstOrDefaultAsync(pr => pr.Id == depratmentID);

            getDepartment!.Name = changeDepartment.Name;
            getDepartment!.Description = changeDepartment.Description;

            await _database.SaveChangesAsync();
            _database.SaveChangesFailed += DatabaseFailed.SaveChangesFailed;

            return new DepartmentResponse()
            {
                Result = true,
                Message = "Well done"
            };
        }
    }
}
