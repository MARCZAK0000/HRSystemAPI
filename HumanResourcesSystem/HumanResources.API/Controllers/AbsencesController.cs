using HumanResources.Application.CQRS_Absence.Command;
using HumanResources.Application.CQRS_Absence.Handler;
using HumanResources.Domain.AbsenceModelDto;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.API.Controllers
{
    [ApiController]
    [Route("api/absence")]
    public class AbsencesController : ControllerBase
    {
        private readonly IAbsenceHandlerService _absenceHandlerService;

        private readonly IAbsenceCommandService _absenceCommandService;

        public AbsencesController(IAbsenceHandlerService absenceHandlerService, IAbsenceCommandService absenceCommandService)
        {
            _absenceHandlerService = absenceHandlerService;
            _absenceCommandService = absenceCommandService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAbsence([FromBody] CreateAbsenceDto createAbsence, CancellationToken token)
        {
            var result = await _absenceCommandService.CreateAbsenceAsync(createAbsence, token);

            return Ok(result);
        }

        [HttpGet("info/{year}")]
        public async Task<IActionResult> ShowAbsencesAllByYear([FromRoute] int year, [FromQuery] string userID, CancellationToken token)
        {
            var result = await _absenceHandlerService.ShowAbsencesByYearAsync(userID, year, token);

            return Ok(result);  
        }
        [HttpGet("info")]
        public async Task<IActionResult> ShowAbsenceById([FromQuery] ShowAbsenceByIdDto update, CancellationToken token)
        {
            var result = await _absenceHandlerService.ShowAbsenceByIDAsync(userCode: update.UserCode, absenceId: update.AbsenceId, token: token);

            return Ok(result);
        }
        [HttpPut("leader")]
        public async Task<IActionResult> AbsenceDecision([FromBody] AbsenceDecisionInfoDto infoDto, CancellationToken token)
        {
            var result = await _absenceCommandService.AbsenceDecisionAsync(infoDto, token);

            return Ok(result);
        }

        [HttpGet("info/report")]
        public async Task<IActionResult> GenerateAbsenceReport([FromQuery]string userId, [FromQuery] int year, CancellationToken token)
        {
            var result = await _absenceHandlerService.GeneratePdfReportAsync(userId, year, token);

            return new FileStreamResult(result, "application/pdf");
        }
    }
}
