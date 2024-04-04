using HumanResources.Domain.AttendanceModelDto;
using HumanResources.Domain.Entities;
using HumanResources.Domain.UserModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.Repository
{
    public interface IAttendanceRepository
    {
        Task<bool> UserArrivalAsync(UserArrivalDto userArrival);

        Task<List<Arrivals>> GetUserAttendanceByMonthAsync(GetAttendanceByMonthDto monthDto);

        Task<bool> UserDepartureAsync(UserDepartureDto userDeparture);

        Task<Arrivals> GetUserAttendanceByDateAsync(DateTime date);

        Task<GetAttendanceStatsDto> GetUserAttendanceStatsByMontAsync(GetAttendanceByMonthDto month);

        Task <List<Arrivals>> GetUserCompletedOrNotAttendenceByMonthAsync(GetAttendanceByMonthDto monthDto, bool isCompleted);

        Task<GetAttendanceStatsDto> GetInformationsAboutUserForLeadersAttendanceByMonth(GetAttendanceByMonthDto monthDto, string UserCode);

    }
}
