using HumanResources.Domain.Enums;

namespace HumanResources.Domain.AbsenceModelDto
{
    public class AbsenceInfoDto
    {
        public string Name { get; set; }

        public string AbsenceTypeName { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public decimal PeriodOfTime { get; set; }

        public bool IsAccepted { get; set; }

        public bool Declined { get; set; }

    }
}
