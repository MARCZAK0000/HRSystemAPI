using HumanResources.Domain.Enums;

namespace HumanResources.Domain.CalculateDays
{
    public class CalculateDaysInfo
    {

        public int InitialDays { get; set; }

        public int BonusDays { get; set; }

        public int RequirmentYears { get; set; }

        public EducationLevel Level { get; set; }
    }
}
