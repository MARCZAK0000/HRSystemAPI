using HumanResources.Domain.AttendanceModelDto;
using HumanResources.Domain.Repository;
using HumanResources.Domain.UserModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Application.CQRS_Attendance.Handler
{
    public interface IAttendanceHandlerService
    {
        public Task<List<GetArrivalsDto>> GetUserAttendanceByMonthAsync(GetAttendanceByMonthDto monthDto);

        public Task<GetArrivalsDto> GetUserAttendanceByDateAsync(DateTime date);

        public Task<GetAttendanceStatsDto> GetUserAttendanceStatsByMontAsync(GetAttendanceByMonthDto montDto);
    }
}
