using HumanResources.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.ModelDtos
{
    public class UpdateAccountInformationsDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public EducationLevel EducationLevel { get; set; }

        public int YearsOfExperiences { get; set; }
    }
}
