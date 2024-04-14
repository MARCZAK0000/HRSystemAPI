using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.Entities
{
    public class AbsencesType
    {

        public int Id { get; set; } 

        public string Name { get; set; }

        public List<Absence> Absence { get; set; }
    }
}
