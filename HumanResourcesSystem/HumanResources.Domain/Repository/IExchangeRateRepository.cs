using HumanResources.Domain.JsonResponse;

namespace HumanResources.Domain.Repository
{
    public interface IExchangeRateRepository
    {
        Task<List<CurrenciesResponse>> AddRatesToDbAsync(string currency, CancellationToken token);

        Task<List<CurrenciesResponse>> UpdateRatesAsync(string currency, CancellationToken token);
    }
}
