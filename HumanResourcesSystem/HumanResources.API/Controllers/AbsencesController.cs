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
        public async Task<IActionResult> CreateAbsence([FromBody] CreateAbsenceDto createAbsence)
        {
            var result = await _absenceCommandService.CreateAbsenceAsync(createAbsence);

            return Ok(result);
        }

        [HttpGet("info/{year}")]
        public async Task<IActionResult> ShowAbsencesAllByYear([FromRoute] int year)
        {
            var result = await _absenceHandlerService.ShowAbsencesByYearAsync(year);

            return Ok(result);  
        }

        [HttpPut("leader")]
        public async Task<IActionResult> AbsenceDecision([FromBody] AbsenceDecisionInfoDto infoDto)
        {
            var result = await _absenceCommandService.AbsenceDecisionAsync(infoDto);

            return Ok(result);
        }
    }
}
