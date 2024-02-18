using HumanResources.Domain.Enums;

namespace HumanResources.Domain.Entities
{
    public class UserInfo
    {
        public int Id { get; set; }

        public User User { get; set; }

        public string UserId {  get; set; }    

        public string Name { get; set; }

        public string LastName { get; set; } 
        
        public string Email { get; set; }

        public string Phone { get; set; }

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
                InitialDays - DaysOfAbsencesCurrentYear
                : (InitialDays + InitialBonus) - DaysOfAbsencesCurrentYear;
        }

        public List<Absence>? Absences { get; set; }

        public virtual Departments Department { get; set; }

        public int DepartmentID { get; set; } = 9;

    }
}
