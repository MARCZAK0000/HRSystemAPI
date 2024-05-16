using HumanResources.Domain;
using HumanResources.Domain.JsonResponse;
using HumanResources.Domain.Repository;

namespace HumanResources.Application.CQRS_Financial.CQRS_ExchangeRate.Command
{
    public class ExchangeRateCommandServices : IExchangeRateCommandServices
    {
        private readonly IExchangeRateRepository _reposiotry;

        public ExchangeRateCommandServices(IExchangeRateRepository reposiotry)
        {
            _reposiotry = reposiotry;
        }

        public async Task<List<CurrenciesResponse>> AddRatesToDbAsync(string currencyCode, CancellationToken token) => 
            await _reposiotry.AddRatesToDbAsync(currencyCode, token);


        public async Task<List<CurrenciesResponse>> UpdateRatesAsync(string currencyCode, CancellationToken token) =>
            await _reposiotry.UpdateRatesAsync(currencyCode, token);
        
    }
}
