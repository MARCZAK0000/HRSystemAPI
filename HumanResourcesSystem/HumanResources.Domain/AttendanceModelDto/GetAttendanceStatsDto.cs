using HumanResources.Domain.Entities;
using HumanResources.Domain.UserModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.AttendanceModelDto
{
    public class GetAttendanceStatsDto
    {
        public GetAttendanceStatsDto(int completedDays, int notCompletedDays, int daysCount, List<GetArrivalsDto> listOfCompletedDays)
        {
            CompletedDays = completedDays;
            NotCompletedDays = notCompletedDays;
            ListOfCompletedDays = listOfCompletedDays;
            DaysCount = daysCount;
        }

        public int? CompletedDays { get; set; }

        public int? NotCompletedDays { get; set; }   

        public int? DaysCount { get; set; } 

        public List<GetArrivalsDto>? ListOfCompletedDays { get; set; }

        
    }
}
