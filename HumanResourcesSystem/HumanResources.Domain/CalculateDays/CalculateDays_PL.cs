using HumanResources.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.CalculateDays
{
    public class CalculateDays_PL : CalculateBase
    {
        public CalculateDays_PL(int initialDays, int bonusDays, int requirmentYears,  EducationLevel level) 
        {
            InitialDays = initialDays;
            BounsDays = bonusDays;
            RequirementDays = requirmentYears;
            Level = level;
        }

        public override int CalculateDays(int daysOfAbsenceCurrentYears, int yearsOfExpierience)
        {
            var CalculateYearsOfExperiences = yearsOfExpierience;

            switch (Level)
            {
                case EducationLevel.Primary:
                    CalculateYearsOfExperiences += 0;
                    break;

                case EducationLevel.Secondary:
                    CalculateYearsOfExperiences += 4;
                    break;

                case EducationLevel.Higher:
                    CalculateYearsOfExperiences += 8;
                    break;

                default:
                    CalculateYearsOfExperiences += 0;
                    break;
            }

            var DaysOfAbsencesToUse = CalculateYearsOfExperiences >= 10 ?
                (InitialDays + BounsDays) - daysOfAbsenceCurrentYears
                : InitialDays - daysOfAbsenceCurrentYears;

            return DaysOfAbsencesToUse;
        }
    }
}
