using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.Cache
{
    public static class CacheKeys
    {
        //AbsencesHandler
        public static string ShowAbsencesByYearCache => nameof(ShowAbsencesByYearCache);

        //ArrivalsHandler
        public static string GetUserAttendanceByMonthAsync => nameof(GetUserAttendanceByMonthAsync);

        public static string GetUserAttendanceByDateAsync => nameof(GetUserAttendanceByDateAsync);

        public static string GetUserCompletedOrNotAttendenceByMonthAsync => nameof(GetUserCompletedOrNotAttendenceByMonthAsync);

        public static string GetUserAttendanceStatsByMontAsync => nameof(GetUserAttendanceStatsByMontAsync);

        public static string GetInformationsAboutUserForLeadersAttendanceByMonth => nameof(GetInformationsAboutUserForLeadersAttendanceByMonth);
        //DeparmentHandler
        public static string DepartmentInfoCache => nameof(DepartmentInfoCache);

        public static string GetAllDeparmentsCache => nameof(GetAllDeparmentsCache);

    }
}
