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

        public int AbsenceId { get; set; }  

        public AbsencesType AbsencesType { get; set; }

        public string UserId { get; set; }

        public UserInfo User { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int PeriodOfTime { get;  private set; }

        public bool isAccepted { get; set; } = false;

        public void CalculatePeriodOfTime() => PeriodOfTime = EndTime.Subtract(StartTime).Days + 1;
    }
}
