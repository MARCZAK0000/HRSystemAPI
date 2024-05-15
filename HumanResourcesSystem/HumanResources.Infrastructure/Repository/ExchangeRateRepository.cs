using HumanResources.Domain.Entities;
using HumanResources.Domain.Events;
using HumanResources.Domain.Exceptions;
using HumanResources.Domain.JsonResponse;
using HumanResources.Domain.Repository;
using HumanResources.Infrastructure.Authentication;
using HumanResources.Infrastructure.Database;
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

        public ExchangeRateRepository(ExchangeRateAPIAuthenticationSettings apiKey, HumanResourcesDatabase database)
        {
            _apiKey = apiKey;
            _database = database;
        }


        public async Task<List<CurrenciesResponse>> AddRatesToDbAsync(string currencyCode)
        {
            var count = await _database
                .ExchangeRates
                .Where(pr=>pr.NameFrom==currencyCode)
                .CountAsync();

            if(count >= 3)
            {
                throw new BadRequestException("Use Update rather AddToDb ");
            }

            var result = await GetValueFromApiAsync(currencyCode);


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
                                               
                    });
            });

            await _database.SaveChangesAsync();
            _database.SaveChangesFailed += DatabaseFailed.SaveChangesFailed;
            return result;
        }

        public async Task<List<CurrenciesResponse>> UpdateRatesAsync(string currency)
        {
            var result = await GetValueFromApiAsync(currency);

            var getCurrency = await _database
                .ExchangeRates
                .Where(pr => pr.NameFrom == currency)
                .ToListAsync();


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

            await _database.SaveChangesAsync();
            _database.SaveChangesFailed += DatabaseFailed.SaveChangesFailed;
            
            return result;
        }

        private static List<CurrenciesResponse> GetValueFromJSON(string jsonString, List<string> currencies)
        {

            var json = JsonConvert.DeserializeObject(jsonString) as JObject;
            var data = json!["data"];
            var result = new List<CurrenciesResponse>();

            for (int i = 0; i < currencies.Count; i++)
            {
                var currentIndex = currencies[i].Replace(",",string.Empty);
                var currency = data[$"{currentIndex}"];

                result.Add(new CurrenciesResponse()
                {
                    code = currency["code"].Value<string>(),
                    value = currency["value"].Value<double>(),
                });
            }


            return result;
        }

        private async Task<List<CurrenciesResponse>> GetValueFromApiAsync(string currencyCode)
        {
            var url = $"https://api.currencyapi.com/v3/latest?apikey={_apiKey.API_KEY}&base_currency={currencyCode}&currencies=";
            var listOfCurrencies = new List<string>() { "PLN,", "USD," , "EUR" };

            listOfCurrencies.ForEach(item => url += item);
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new BadRequestException("Request has an error");
            }
            var data = await response.Content.ReadAsStringAsync();

            var result = GetValueFromJSON(data, listOfCurrencies);

            if (!result.Any())
            {
                throw new NotFoundException("There is a problem with JSON conversion or response is empty");
            }

            return result;
        }

    }
}
