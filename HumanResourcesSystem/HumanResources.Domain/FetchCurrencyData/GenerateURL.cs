using System;

namespace HumanResources.Domain.CurrencyData
{
    public static class GenerateURL
    {
        public static string URL(string API_KEY, List<string> currencies, string currencyCode)
        {
            var url = $"https://api.currencyapi.com/v3/latest?apikey={API_KEY}&base_currency={currencyCode}&currencies=";
            for (int i = 0; i < currencies.Count; i++)
            {
                if (i == currencies.Count - 1)
                {
                    url += $"{currencies[i]}";
                    break;
                }
                url += $"{currencies[i]},";
            }

            return url;
        }
    }
}
