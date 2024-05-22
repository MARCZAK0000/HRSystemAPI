using HumanResources.Domain.Enums;

namespace HumanResources.Domain.CalculateDays
{
    public abstract class CalculateBase
    {
        protected int InitialDays { get; set; }

        protected int BounsDays { get; set; }

        protected int RequirementDays { get; set; }

        protected EducationLevel Level { get; set; }

        public abstract int CalculateDays(int daysOfAbsenceCurrentYears, int yearsOfExpierience);
    }
}
