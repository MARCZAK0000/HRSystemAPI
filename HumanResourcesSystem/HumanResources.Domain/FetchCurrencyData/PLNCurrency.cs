using HumanResources.Domain.JsonResponse;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.CurrencyData
{
    public class PLNCurrency : Currency
    {
        public override async Task<List<CurrenciesResponse>> GetResponseAsync(string API_KEY, CancellationToken token)
        {
            var url = GenerateURL.URL(API_KEY, listOfCurrencies, "PLN");
            var data = await CurrencyApi.CurrencyApi_Data(url, token);
            var response = DeserializeJSON.Deserialize(data, listOfCurrencies);
            return response;
        }
    }
}
