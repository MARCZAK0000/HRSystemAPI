using HumanResources.Application.CQRS_Attendance.Command;
using HumanResources.Application.CQRS_Attendance.Handler;
using HumanResources.Domain.AttendanceModelDto;
using HumanResources.Domain.UserModelDto;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.API.Controllers
{
    [Route("api/attendance")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {

        private readonly IAttendanceCommandService _attendanceCommandService;

        private readonly IAttendanceHandlerService _attendanceHandlerService;

        public AttendanceController(IAttendanceCommandService attendanceCommandService, 
            IAttendanceHandlerService attendanceHandlerService)
        {
            _attendanceCommandService = attendanceCommandService;
            _attendanceHandlerService = attendanceHandlerService;
        }


        [HttpPost]
        public async Task<IActionResult> UserArrival([FromBody] UserArrivalDto userArrival, CancellationToken token)
        {
            var result = await _attendanceCommandService.UserArrivalAsync(userArrival, token);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UserDeparture([FromBody] UserDepartureDto userDeparture, CancellationToken token)
        {
            var result = await _attendanceCommandService.UserDepartureAsync(userDeparture, token);

            return Ok(result);  
        }

        [HttpGet("info/date")]
        public async Task<IActionResult> GetUserAttendanceByDate([FromQuery] DateTime date, CancellationToken token)
        {
            var result = await _attendanceHandlerService.GetUserAttendanceByDateAsync(date, token);

            return Ok(result);
        }

        [HttpGet ("info/month")]
        public async Task<IActionResult> GetUserAttendanceByMonth([FromQuery] GetAttendanceByMonthDto monthDto, CancellationToken token)
        {
            var result = await _attendanceHandlerService.GetUserAttendanceByMonthAsync(monthDto, token);

            return Ok(result);  
        }

        [HttpGet ("stats")]
        public async Task<IActionResult> GetUserAttendanceCountByMonth([FromQuery] GetAttendanceByMonthDto monthDto, CancellationToken token)
        {
            var result = await _attendanceHandlerService.GetUserAttendanceStatsByMontAsync(monthDto, token);

            return Ok(result);
        }
        [HttpGet("stats/days")]
        public async Task<IActionResult> GetUserCompletedOrNotDaysByMonth([FromQuery] GetAttendanceByMonthDto monthDto, [FromQuery]bool IsCompleted, CancellationToken token)
        {
            var result = await _attendanceHandlerService.GetUserCompletedOrNotAttendenceByMonthAsync(monthDto, IsCompleted, token);

            return Ok(result);
        }

        [HttpGet("info/user")]
        public async Task<IActionResult> GetInformationsAboutUserAttendanceByMonth([FromQuery] GetAttendanceByMonthDto monthDto, [FromQuery] string userCode, CancellationToken token)
        {
            var result = await _attendanceHandlerService.GetInformationsAboutUserAttendanceByMonth(monthDto, userCode, token); 
            return Ok(result);
        }



    }
}
