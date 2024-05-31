using HumanResources.Application.CQRS_AdditionalHours.Command;
using HumanResources.Application.CQRS_AdditionalHours.Handler;
using HumanResources.Domain.AdditionalHoursDto;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.API.Controllers
{
    [ApiController]
    [Route("api/additionalhours")]
    public class AdditionalHoursController : ControllerBase
    {
        private readonly IAdditionalHoursCommandServices _additionalHoursCommand;

        private readonly IAdditionalHoursHandlerServices _additionalHoursHandler;

        public AdditionalHoursController(IAdditionalHoursCommandServices additionalHoursCommand, IAdditionalHoursHandlerServices additionalHoursHandler)
        {
            _additionalHoursCommand = additionalHoursCommand;
            _additionalHoursHandler = additionalHoursHandler;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAdditionalHours([FromBody ]CreateAdditionalHoursRequestDto hours, CancellationToken token)
        {
            var result = await _additionalHoursCommand.CreateAdditionalHoursRequestAsync(hours, token);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAdditionalHoursRequest([FromBody] UpdateAdditionalHoursRequestDto hours, CancellationToken token)
        {
            var result = await _additionalHoursCommand.UpdateAdditionalHoursRequestAsync(hours, token);

            return Ok(result);  
        }
        [HttpGet("/info")]
        public async Task<IActionResult> ShowAdditionalHoursRequest([FromQuery] ShowAdditionalHoursDto hours, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpGet("/info/all")]
        public async Task<IActionResult> ShowAllAdditionalHoursRequest([FromQuery] ShowAllAdditionalHoursDto hours, CancellationToken token)
        {
            var result = await _additionalHoursHandler.ShowAllAdditionalHoursRequestAsync(hours, token);

            return Ok(result);
        }
        [HttpGet("/info/date")]
        public async Task<IActionResult> ShowAdditionalHoursRequestByDate([FromQuery] ShowAdditionalHoursDateDto hours, CancellationToken token)
        {
            throw new NotImplementedException();
        } 
    }
}
