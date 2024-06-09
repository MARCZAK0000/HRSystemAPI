using HumanResources.API.Controllers;
using HumanResources.Domain.CalculateDays;
using HumanResources.Domain.Enums;

namespace HumanResources.Domain.Entities
{
    public class UserInfo
    {

        public string UserId {  get; set; }  
        public string Name { get; set; }
        public string LastName { get; set; } 
        public string UserCode { get; set; }
        public int? DaysOfAbsencesToUse { get; set; }
        public int? DaysOfAbsencesCurrentYear { get; set; } = 0;
        public int? YearsOfExperiences { get; set; }
        public EducationLevel? EducationTitle { get; set; }
        public int DepartmentID { get; set; } = 9;
        public bool IsSupervisior {get; set; } = false;
        public string? RelativePhotoPath { get; set; }

        public User User { get; set; }
        public virtual List<Absence>? Absences { get; set; }
        public virtual Departments Department { get; set; }
        public virtual List<Attendance> Arrivals { get; set; }
        public virtual EmployeePay? EmployeePay { get; set; }
        public virtual List<EmployeePayHistory>? EmployeePayHistory { get; set; }
        public virtual List<AdditionalHours>? AdditionalHours { get; set;}
        public virtual List<AdditionalHours>? AdditionalHoursSuperVisor { get; set;}
    }
}
