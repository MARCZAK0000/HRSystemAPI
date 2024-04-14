using HumanResources.Domain.Entities;
using HumanResources.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.AbsenceModelDto
{
    public class CreateAbsenceDto
    {
        public string Name { get; set; }

        public int AbsenceTypeId { get; set; }

        public DateTime CreatedTime { get; set; } = DateTime.Now;

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
