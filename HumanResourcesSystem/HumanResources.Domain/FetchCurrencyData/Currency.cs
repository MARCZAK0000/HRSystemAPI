using HumanResources.Domain.JsonResponse;

namespace HumanResources.Domain.CurrencyData
{
    public abstract class Currency
    {
        public abstract Task<List<CurrenciesResponse>> GetResponseAsync(string API_KEY, CancellationToken token);

        public List<string> listOfCurrencies = new() { "PLN", "USD", "EUR" };

    }
}
