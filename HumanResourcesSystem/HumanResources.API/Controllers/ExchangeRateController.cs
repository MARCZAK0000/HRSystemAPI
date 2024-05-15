using HumanResources.Application.CQRS_Financial.CQRS_ExchangeRate.Command;
using HumanResources.Application.CQRS_Financial.CQRS_ExchangeRate.Handler;
using HumanResources.Domain.FinancialModelDto;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.API.Controllers
{
    [ApiController]
    [Route("api/exchange")]
    public class ExchangeRateController:ControllerBase
    {
        private readonly IExchangeRateCommandServices _exchangeRateCommandServices;
        private readonly IExchangeRateHandlerServices _exchangeRateHandlerServices;

        public ExchangeRateController(IExchangeRateCommandServices exchangeRateCommandServices, IExchangeRateHandlerServices exchangeRateHandlerServices)
        {
            _exchangeRateCommandServices = exchangeRateCommandServices;
            _exchangeRateHandlerServices = exchangeRateHandlerServices;
        }

        [HttpPost]
        public async Task<IActionResult> AddRatesToDbAsync([FromBody] GetExchangeRateAsyncDto getExchange)
        {
            var result = await _exchangeRateCommandServices.AddRatesToDbAsync(getExchange.CurrencyCode);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRatesAsync([FromBody] GetExchangeRateAsyncDto getExchange)
        {
            var result = await _exchangeRateCommandServices.UpdateRatesAsync(getExchange.CurrencyCode);

            return Ok(result);
        }
    }
}
