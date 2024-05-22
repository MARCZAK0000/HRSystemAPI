using HumanResources.Application.CQRS_Financial.CQRS_Pays.Command;
using HumanResources.Application.CQRS_Financial.CQRS_Pays.Handler;
using HumanResources.Domain.FinancialModelDto;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.API.Controllers
{
    [ApiController]
    [Route("api/financial")]
    public class FinancialController : ControllerBase
    {
        private readonly IEmployeePaysCommandServices _commandService;

        private readonly IEmployeePaysHandlerServices _handlerService;

        public FinancialController(IEmployeePaysCommandServices commandService, IEmployeePaysHandlerServices handlerService)
        {
            _commandService = commandService;
            _handlerService = handlerService;
        }

        [HttpPost("set")]
        public async Task<IActionResult> SetUserPaymentRate([FromBody] SetUserPaymentRateDto setUserPayment, CancellationToken token)
        {
            var result = await _commandService.SetUserPaymentRateAsync(setUserPayment, token);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUserPaymentRate([FromBody] SetUserPaymentRateDto setUserPayment, CancellationToken token)
        {
            var result = await _commandService.UpdateUserPaymentRateAsync(setUserPayment, token);
            return Ok(result);
        }
    }
}
