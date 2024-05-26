using HumanResources.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.ModelDtos
{
    public class UpdateExperienceInfomrationsDto
    {
        public EducationLevel Level { get; set; }

        public int YearsOfExperience {  get; set; } 
    }
}
