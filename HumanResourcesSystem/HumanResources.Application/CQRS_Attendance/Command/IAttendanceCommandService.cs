using HumanResources.Domain.UserModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Application.CQRS_Attendance.Command
{
    public interface IAttendanceCommandService
    {
        Task<bool> UserArrivalAsync(UserArrivalDto userArrival);

        Task<bool> UserDepartureAsync(UserDepartureDto userDeparture);

    }
}
