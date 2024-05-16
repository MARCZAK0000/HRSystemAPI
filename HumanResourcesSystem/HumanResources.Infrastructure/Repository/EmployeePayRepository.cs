using HumanResources.Application.Authentication;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Enums;
using HumanResources.Domain.Exceptions;
using HumanResources.Domain.FinancialModelDto;
using HumanResources.Domain.Repository;
using HumanResources.Domain.Response;
using HumanResources.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace HumanResources.Infrastructure.Repository
{
    public class EmployeePayRepository : IEmployeePayRepository
    {
        private readonly UserManager<User> _userManager;

        private readonly HumanResourcesDatabase _database;

        private readonly IUserContext _userContext;

        public EmployeePayRepository(UserManager<User> userManager, 
            HumanResourcesDatabase database,
            IUserContext userContext)
        {
            _userManager = userManager;
            _database = database;
            _userContext = userContext;
        }

        public async Task<SetPaymentRateResponse> SetUserPaymentRateAsync(SetUserPaymentRateDto setUserPaymentRate, CancellationToken token)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id)??
                throw new InvalidEmailOrPasswordExcepiton("SetUserPaymentRate: Invalid Email or password");
        
            if(!await _userManager.IsInRoleAsync(user, nameof(RolesEnum.Manager)))
            {
                throw new ForbidenException("SetUserPaymentRate: you don't have permission to that request");
            }

            var findUser = await _database.UserInfo
                .Where(pr => pr.UserCode == setUserPaymentRate.UserCode)
                .Select(pr => pr.UserId)
                .FirstOrDefaultAsync(token)??
                throw new NotFoundException($"SetUserPaymentRate: user with usercode${setUserPaymentRate.UserCode} is not found");

            var userPayment = new EmployeePay()
            {
                UserID = findUser,
                RatePLN = setUserPaymentRate.RatePLN,
                ModifiedDate = DateTime.Now,
            };

            var currentExchagneRate = await _database
                .ExchangeRates
                .Where(pr=>pr.NameFrom == "PLN")
                .Select(x=> new
                {
                    Value = x.Rate,
                    Name = x.NameTo
                })
                .ToListAsync(token);
            if (!currentExchagneRate.Any())
            {
                throw new BadRequestException("SetUserPaymentRate: we didn't found currency Rate in Db");
            }

            var rateEur = currentExchagneRate.Where(pr => pr.Name == "EUR").Select(pr => pr.Value).FirstOrDefault();
            var rateUSD = currentExchagneRate.Where(pr => pr.Name == "USD").Select(pr=>pr.Value).FirstOrDefault();

            userPayment.RateEURO = userPayment.CalculateRate((amount, rate) =>
            {
                return rate * amount;
            }, setUserPaymentRate.RatePLN, rateEur);

            userPayment.RateUSD = userPayment.CalculateRate((amount, rate) =>
            {
                return rate * amount;
            }, setUserPaymentRate.RatePLN, rateUSD);

            await _database.AddAsync(userPayment, token);
            await _database.SaveChangesAsync(token);
            //var getRates = await _database.
          
            return new SetPaymentRateResponse
            {
                RatePLN = setUserPaymentRate.RatePLN,
                UserCode = setUserPaymentRate.UserCode,
                RateEuro = userPayment.RateEURO,
                RateUSD = userPayment.RateUSD

                
            };
        }

        public async Task<SetPaymentRateResponse> UpdateUserPaymentRateAsync(SetUserPaymentRateDto updateUserPaymentRate, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
