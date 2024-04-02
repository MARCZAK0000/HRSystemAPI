using HumanResources.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.AttendanceModelDto
{
    public class GetAttendanceByMonthDto
    {
        public MonthEnum Month { get; set; }

        public int Year { get; set; }
    }
}
