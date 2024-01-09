using HumanResources.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.Entities
{
    public class Absence
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public AbsenceType AbsenceType { get; set; }

        public int UserId { get; set; }

        public UserInfo User { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public decimal PeriodOfTime { get;  private set; }

        public void CalculatePeriodOfTime() => PeriodOfTime = EndTime.Subtract(StartTime).Days;
    }
}
