using HumanResources.API.Controllers;
using HumanResources.Domain.Enums;

namespace HumanResources.Domain.Entities
{
    public class UserInfo
    {
        public string UserId {  get; set; }  
        
        public User User { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; } 

        public string UserCode { get; set; }
        
        public int? DaysOfAbsencesToUse { get; set; }

        public int? DaysOfAbsencesCurrentYear { get; set; } = 0;

        public int? YearsOfExperiences { get; set; }

        public EducationLevel? EducationTitle { get; set; }

        public void CalculateDaysOfAbsences()
        {
            const int InitialDays = 20;
            const int InitialBonus = 8;

            var CalculateYearsOfExperiences = YearsOfExperiences;

            switch (EducationTitle)
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

            DaysOfAbsencesToUse = CalculateYearsOfExperiences >= 10 ?
                (InitialDays + InitialBonus) - DaysOfAbsencesCurrentYear
                : InitialDays - DaysOfAbsencesCurrentYear;
        }

        public virtual List<Absence>? Absences { get; set; }

        public virtual Departments Department { get; set; }

        public virtual List<Arrivals> Arrivals { get; set; }

        public int DepartmentID { get; set; } = 9;

        public virtual EmployeePay? EmployeePay { get; set; }

        public virtual List<EmployeePayHistory>? EmployeePayHistory { get; set; }
    }
}
