using HumanResources.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.AbsenceModelDto
{
    public class AbsenceDecisionDto
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserCode { get; set; }

        public string AbsenseTitle { get; set; }

        public int AbsenceId { get; set; }

        public string AbsenceName { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int PeriodOfTime { get; set;  }

        public bool IsAccepted { get; set; }

        public bool Declined { get; set; }


    }
}
