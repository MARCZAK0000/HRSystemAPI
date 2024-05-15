using HumanResources.Domain;
using HumanResources.Domain.JsonResponse;

namespace HumanResources.Application.CQRS_Financial.CQRS_ExchangeRate.Command
{
    public interface IExchangeRateCommandServices
    {
        Task<List<CurrenciesResponse>> AddRatesToDbAsync(string currencyCode);

        Task<List<CurrenciesResponse>> UpdateRatesAsync(string currencyCode);   
    }
}
