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
        Task<bool> UserArrivalAsync(UserArrivalDto userArrival, CancellationToken token);

        Task<List<Arrivals>> GetUserAttendanceByMonthAsync(GetAttendanceByMonthDto monthDto, CancellationToken token);

        Task<bool> UserDepartureAsync(UserDepartureDto userDeparture, CancellationToken token);

        Task<Arrivals> GetUserAttendanceByDateAsync(DateTime date, CancellationToken token);

        Task<GetAttendanceStatsDto> GetUserAttendanceStatsByMontAsync(GetAttendanceByMonthDto month, CancellationToken token);

        Task <List<Arrivals>> GetUserCompletedOrNotAttendenceByMonthAsync(GetAttendanceByMonthDto monthDto, bool isCompleted, CancellationToken token);

        Task<GetAttendanceStatsDto> GetInformationsAboutUserForLeadersAttendanceByMonth(GetAttendanceByMonthDto monthDto, string UserCode, CancellationToken token);

    }
}
