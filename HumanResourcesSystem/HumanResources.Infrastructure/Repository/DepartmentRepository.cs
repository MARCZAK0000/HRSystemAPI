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

        public async Task<DepartmentResponse> ChangeUserDepartmentAsync(ChangeDepartmentDto changeDepartment, CancellationToken token)
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
                .FirstOrDefaultAsync(pr => pr.UserCode == changeDepartment.UserCode, cancellationToken: token) ??
                throw new NotFoundException($"We cannot found user with that UserCode: {changeDepartment.UserCode}");

            getUser.DepartmentID = changeDepartment.DepartmentId;

            await _database.SaveChangesAsync(token);
            _database.SaveChangesFailed += DatabaseFailed.SaveChangesFailed;
            return new DepartmentResponse()
            {
                DepartmentId = changeDepartment.DepartmentId,
                Result = true,
                Message = "Well done"
            };
        }

        public async Task<Departments> DepartmentInfoAsync(int departmentId, CancellationToken token)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            if (!await _userManager.IsInRoleAsync(user, nameof(RolesEnum.Leader)))
            {
                throw new ForbidenException("Forbiden Request");
            }


            //var result = await _database.Departments
            //    .Include(pr => pr.Users).ThenInclude(pr=>pr.User)
            //    .FirstOrDefaultAsync(pr => pr.Id == departmentId, cancellationToken: token) ??
            //    throw new NotFoundException($"We cannot find department with DepartmentID: {departmentId}");


            var resultBase = _database.Departments
                .Where(pr => pr.Id == departmentId);

            var result = await resultBase
                .Include(pr => pr.Users!)
                .ThenInclude(pr => pr.User)
                .FirstOrDefaultAsync(cancellationToken: token);
                
            return result!;

        }

        public async Task<DepartmentResponse> AddDepartmentAsync(DepartmentUpdateDto departmentInfo, CancellationToken token)
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
                .AddAsync(newDepartment, token);

            await _database.SaveChangesAsync(token);
            _database.SaveChangesFailed += DatabaseFailed.SaveChangesFailed;


            return new DepartmentResponse()
            {
                Result = true,
                Message = "Well Done"
            };
        }

     
        public async Task<DepartmentResponse> UpdateDepartmentAsync(DepartmentUpdateDto changeDepartment, int depratmentID, CancellationToken token)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            if (!await _userManager.IsInRoleAsync(user, nameof(RolesEnum.Admin)))
            {
                throw new ForbidenException("Forbiden Request");
            }

            var getDeparmentsCount = await _database.Departments
                .CountAsync(cancellationToken: token);

            if(depratmentID < 1 || depratmentID > getDeparmentsCount) 
            {
                throw new BadRequestException($"Invalid index of department: 1 - {getDeparmentsCount}");
            }

            var getDepartment = await _database.
                Departments.
                FirstOrDefaultAsync(pr => pr.Id == depratmentID, cancellationToken: token);

            getDepartment!.Name = changeDepartment.Name;
            getDepartment!.Description = changeDepartment.Description;

            await _database.SaveChangesAsync(token);
            _database.SaveChangesFailed += DatabaseFailed.SaveChangesFailed;

            return new DepartmentResponse()
            {
                Result = true,
                Message = "Well done"
            };
        }

        public async Task<List<Departments>> GetAllDeparmentsAsync(CancellationToken token)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            var deparments = await _database.Departments
                .ToListAsync(cancellationToken: token);

            return deparments;
        }


        public async Task<DepartmentEmployee> GetUserDeparmentsEmpolyeeAsync(CancellationToken token)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id);

            var userDeparmentID = await _database
                .UserInfo
                .Where(pr => pr.UserId == user!.Id)
                .Select(pr => pr.DepartmentID)
                .FirstOrDefaultAsync(cancellationToken: token);

            var findUsers = await _database
                .Departments
                .Where(pr => pr.Id == userDeparmentID)
                .Include(pr=>pr.Users)
                .Select(pr=> new DepartmentEmployee()
                {
                    DepartmentID = pr.Id,
                    EmployeeInfo = pr.Users!
                    .Where(p=>p.UserId != user!.Id)
                    .Select(p=> new DepartmentEmployeeInfo
                    {
                        LastName = p.LastName,
                        Name = p.Name,
                        UserCode = p.UserCode,
                        UserID = p.UserId

                    }).ToList(),
                })
                .FirstOrDefaultAsync(token)??
                    throw new NotFoundException("We didn't find users with current deparment ID");


            return findUsers;
        }
    }
}
