﻿using HumanResources.Application.Authentication;
using HumanResources.Domain.CalculateDays;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Events;
using HumanResources.Domain.Exceptions;
using HumanResources.Domain.ModelDtos;
using HumanResources.Domain.Repository;
using HumanResources.Domain.StorageAccountModel;
using HumanResources.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly IUserContext _userContext;

        private readonly UserManager<User> _userManager;

        private readonly HumanResourcesDatabase _dbContext;

        private readonly CalculateDays _calculateDays;

        private readonly CalculateFactory _calculateFactory;

        private readonly IBlobClientReposiotry _blobClientReposiotry;

        public UserRepository(IUserContext userContext, 
            UserManager<User> userManager,
            HumanResourcesDatabase dbContext,
            CalculateDays calculateDays, 
            CalculateFactory calculateFactory,
            IBlobClientReposiotry blobClientReposiotry)
        {
            _userContext = userContext;
            _userManager = userManager;
            _dbContext = dbContext; 
            _calculateDays = calculateDays;
            _calculateFactory = calculateFactory;   
            _blobClientReposiotry = blobClientReposiotry;
        }

        public async Task<UserInfo> GetInfromationsAboutUserAsync(CancellationToken token)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id)??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            var result = await _dbContext
                .UserInfo
                .Include(pr=>pr.User)
                .Include(pr => pr.Department)
                .FirstOrDefaultAsync(pr => pr.UserId == user.Id, cancellationToken: token) ??
                throw new NotFoundException("Something went wrong with informations");
                

            return result;
        }


        public async Task<bool> UpdateInformationsAboutUserAsync(UpdateAccountInformationsDto updateAccountInformations, CancellationToken token)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("ChangePassword: We cannot find user with that Email and Password"); ;

            var isAlreadyUser = await _dbContext.UserInfo.FirstOrDefaultAsync(pr => pr.UserId == user.Id, cancellationToken: token);
            var initCalculaton = _calculateFactory.CalculateDays(_calculateDays.Country, new CalculateDaysInfo
            {
                BonusDays = _calculateDays.BonusDays,
                InitialDays = _calculateDays.InitialDays,
                RequirmentYears = _calculateDays.RequirementsYears,
                Level = updateAccountInformations.EducationLevel,
            });
            if (isAlreadyUser != null)
            {
                isAlreadyUser.Name = updateAccountInformations.FirstName;
                isAlreadyUser.LastName = updateAccountInformations.LastName;
                isAlreadyUser.EducationTitle = updateAccountInformations.EducationLevel;
                isAlreadyUser.YearsOfExperiences = updateAccountInformations.YearsOfExperiences;
                isAlreadyUser.DaysOfAbsencesToUse = initCalculaton.CalculateDays(daysOfAbsenceCurrentYears: (int)isAlreadyUser.DaysOfAbsencesCurrentYear!, (int)isAlreadyUser.YearsOfExperiences);
                
                await _dbContext.SaveChangesAsync(token);
                _dbContext.SaveChangesFailed += DatabaseFailed.SaveChangesFailed;
                return true;
            }

            var userInfo = new UserInfo()
            {
                Name = updateAccountInformations.FirstName,
                LastName = updateAccountInformations.LastName,
                EducationTitle = updateAccountInformations.EducationLevel,
                YearsOfExperiences = updateAccountInformations.YearsOfExperiences,
                UserId = user.Id,
                UserCode = user.UserCode
                
            };
            userInfo.DaysOfAbsencesToUse = initCalculaton.CalculateDays(daysOfAbsenceCurrentYears: 0, yearsOfExpierience: (int)userInfo.YearsOfExperiences);

            await _dbContext.UserInfo
                .AddAsync(userInfo, token);

            await _dbContext.SaveChangesAsync(token);
            _dbContext.SaveChangesFailed += DatabaseFailed.SaveChangesFailed;

            return true;
        }


        public async Task<bool> UpdateExperienceInformationsAboutUser(UpdateExperienceInfomrationsDto update, CancellationToken token)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("ChangePassword: We cannot find user with that Email and Password");

            var findUser = await _dbContext
                .UserInfo
                .Where(pr=>pr.UserId == user.Id)
                .FirstOrDefaultAsync(token)??
                throw new NotFoundException("Insert your informations about user, first");

            findUser.EducationTitle = update.Level;
            findUser.YearsOfExperiences = update.YearsOfExperience;

            var initCalculaton = _calculateFactory.CalculateDays(_calculateDays.Country, new CalculateDaysInfo
            {
                BonusDays = _calculateDays.BonusDays,
                InitialDays = _calculateDays.InitialDays,
                RequirmentYears = _calculateDays.RequirementsYears,
                Level = update.Level,
            });

            findUser.DaysOfAbsencesToUse = initCalculaton.CalculateDays
                (daysOfAbsenceCurrentYears: (int)findUser.DaysOfAbsencesCurrentYear!, yearsOfExpierience: (int)findUser.YearsOfExperiences);

            await _dbContext.SaveChangesAsync(token);

            return true;
        }

        public async Task<BlobResponse> UploadUserImageAsync(IFormFile form, CancellationToken token)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("ChangePassword: We cannot find user with that Email and Password");

            if(Path.GetExtension(form.FileName)!=".jpg" && Path.GetExtension(form.FileName) != ".jpeg" && Path.GetExtension(form.FileName) != ".png")
            {
                throw new BadRequestException("Invalid image extension");
            }

            var result = await _blobClientReposiotry.UploadImage(form, user.Id, token);

            if (!result.IsSuccess)
            {
                throw new BadRequestException(result.Message);
            }

            var path = await _dbContext.
                UserInfo.
                Where(pr => pr.UserId == user.Id).
                Select(pr => pr.RelativePhotoPath).
                FirstOrDefaultAsync(token);

            path = result.FileName;

            await _dbContext.SaveChangesAsync(token);
            
            return result;
        }

        public async Task<BlobResponse> UpdateUserImageAsync(IFormFile form, CancellationToken token)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("ChangePassword: We cannot find user with that Email and Password");

            if (Path.GetExtension(form.FileName) != ".jpg" && Path.GetExtension(form.FileName) != ".jpeg" && Path.GetExtension(form.FileName) != ".png")
            {
                throw new BadRequestException("Invalid image extension");
            }

            var findImage = await _dbContext
                .UserInfo
                .Where(pr=>pr.UserId == user.Id)
                .FirstOrDefaultAsync(token)??
                throw new BadRequestException("There is no user");

            if(findImage.RelativePhotoPath == null) 
            {
                throw new BadRequestException("There is no picture");
            }
            var result = await _blobClientReposiotry.UpdateImage(file: form, fileName: findImage.RelativePhotoPath, token: token);
            
            if (!result.IsSuccess) 
            {
                throw new BadRequestException(result.Message);
            }

            findImage.RelativePhotoPath = result.FileName;

            await _dbContext.SaveChangesAsync(cancellationToken: token);
            return result;
        }
    }
}
