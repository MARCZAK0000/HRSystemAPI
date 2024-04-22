using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.DepartmentModelDto
{
    public class DepartmentInfoDto
    {
        public int DeparmentID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<DepartmentUserInfoDto>? Users { get; set; }
    }
}
