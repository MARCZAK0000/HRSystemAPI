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
        public async Task<IActionResult> UserArrival([FromBody] UserArrivalDto userArrival)
        {
            var result = await _attendanceCommandService.UserArrivalAsync(userArrival);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UserDeparture([FromBody] UserDepartureDto userDeparture)
        {
            var result = await _attendanceCommandService.UserDepartureAsync(userDeparture);

            return Ok(result);  
        }

        [HttpGet("info/date")]
        public async Task<IActionResult> GetUserAttendanceByDate([FromQuery] DateTime date)
        {
            var result = await _attendanceHandlerService.GetUserAttendanceByDateAsync(date);

            return Ok(result);
        }

        [HttpGet ("info/month")]
        public async Task<IActionResult> GetUserAttendanceByMonth([FromQuery] GetAttendanceByMonthDto monthDto)
        {
            var result = await _attendanceHandlerService.GetUserAttendanceByMonthAsync(monthDto);

            return Ok(result);  
        }

        [HttpGet ("stats")]
        public async Task<IActionResult> GetUserAttendanceCountByMonth([FromQuery] GetAttendanceByMonthDto monthDto)
        {
            var result = await _attendanceHandlerService.GetUserAttendanceStatsByMontAsync(monthDto);

            return Ok(result);
        }


    }
}
