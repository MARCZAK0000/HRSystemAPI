using HumanResources.Domain.JsonResponse;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HumanResources.Domain.CurrencyData
{
    public static class DeserializeJSON
    {
        public static List<CurrenciesResponse> Deserialize (string jsonString, List<string> currencies)
        {
            var json = JsonConvert.DeserializeObject(jsonString) as JObject;
            var data = json!["data"];
            var result = new List<CurrenciesResponse>();

            for (int i = 0; i < currencies.Count; i++)
            {
                var currentIndex = currencies[i].Replace(",", string.Empty);
                var currency = data[$"{currentIndex}"];

                result.Add(new CurrenciesResponse()
                {
                    code = currency["code"].Value<string>(),
                    value = currency["value"].Value<double>(),
                });
            }

            return result;
        }
    }
}
