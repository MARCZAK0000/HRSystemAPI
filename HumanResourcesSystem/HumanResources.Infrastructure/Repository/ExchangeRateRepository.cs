using HumanResources.Application.Authentication;
using HumanResources.Domain.CurrencyData;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Events;
using HumanResources.Domain.Exceptions;
using HumanResources.Domain.JsonResponse;
using HumanResources.Domain.Repository;
using HumanResources.Infrastructure.Authentication;
using HumanResources.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace HumanResources.Infrastructure.Repository
{
    public class ExchangeRateRepository : IExchangeRateRepository
    {
        private readonly ExchangeRateAPIAuthenticationSettings _apiKey;

        private readonly HumanResourcesDatabase _database;

        private readonly IUserContext _userContext;

        private readonly UserManager<User> _userManager;

        private readonly CurrencyFactory _currencyFactory;

        public ExchangeRateRepository(ExchangeRateAPIAuthenticationSettings apiKey, HumanResourcesDatabase database
            ,IUserContext userContext, UserManager<User> userManager, CurrencyFactory currencyFactory)
        {
            _apiKey = apiKey;
            _database = database;
            _userContext = userContext;
            _userManager = userManager;
            _currencyFactory = currencyFactory;
        }


        public async Task<List<CurrenciesResponse>> AddRatesToDbAsync(string currencyCode, CancellationToken token)
        {
            var currentUser = _userContext.GetCurrentUser();

            var user = await _userManager.FindByIdAsync(currentUser.Id)??
                throw new InvalidEmailOrPasswordExcepiton("Invalid username");

            var count = await _database
                .ExchangeRates
                .Where(pr=>pr.NameFrom==currencyCode)
                .CountAsync(cancellationToken: token);

            if(count >= 3)
            {
                throw new BadRequestException("Use Update rather AddToDb ");
            }
            
            var factory = _currencyFactory.Fetch(currencyCode);

            var result = await factory.GetResponseAsync(_apiKey.API_KEY, token);


            result.ForEach(async item =>
            {

                await _database
                    .ExchangeRates
                    .AddAsync(new ExchangeRate 
                    {
                        NameFrom = currencyCode,
                        NameTo = item.code,
                        Rate = (decimal)item.value,
                        ModifiededDate = DateTime.Now
                                               
                    }, token);
            });

            await _database.SaveChangesAsync(token);
            _database.SaveChangesFailed += DatabaseFailed.SaveChangesFailed;
            return result;
        }

        public async Task<List<CurrenciesResponse>> UpdateRatesAsync(string currency, CancellationToken token)
        {
            var currentUser = _userContext.GetCurrentUser();

            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("Invalid username");


            var factory = _currencyFactory.Fetch(currency);
            var result = await factory.GetResponseAsync(_apiKey.API_KEY, token);

            var getCurrency = await _database
                .ExchangeRates
                .Where(pr => pr.NameFrom == currency)
                .ToListAsync(cancellationToken: token);


            var i = 0;
            getCurrency.ForEach(item =>
            {
                if(i < result.Count)
                {
                    var rate = result.Where(pr=>pr.code==item.NameTo).FirstOrDefault();
                    if(rate != null) 
                    {
                        item.Rate = (decimal)rate.value;
                        item.ModifiededDate = DateTime.Now;
                        i++;
                    }
                }
            });

            await _database.SaveChangesAsync(token);
            _database.SaveChangesFailed += DatabaseFailed.SaveChangesFailed;
            
            return result;
        }
    }
}
