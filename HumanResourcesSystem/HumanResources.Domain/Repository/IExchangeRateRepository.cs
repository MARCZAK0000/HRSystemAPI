using HumanResources.Domain.JsonResponse;

namespace HumanResources.Domain.Repository
{
    public interface IExchangeRateRepository
    {
        Task<List<CurrenciesResponse>> AddRatesToDbAsync(string currency);

        Task<List<CurrenciesResponse>> UpdateRatesAsync(string currency);
    }
}
