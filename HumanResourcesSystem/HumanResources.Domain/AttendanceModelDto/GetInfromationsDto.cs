using HumanResources.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.UserModelDto
{
    public class GetInfromationsDto
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string UserCode { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public bool IsEmailConfirmed { get; set; } 

        public string EducationLevelName { get; set; }

        public int? DaysOfAbsencesToUse { get; set; }

        public int? DaysOfAbsencesCurrentYear { get; set; } = 0;

        public string DepartmentName { get; set; }
    }
}
