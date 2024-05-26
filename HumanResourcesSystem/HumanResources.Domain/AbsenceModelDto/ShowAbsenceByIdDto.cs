using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.AbsenceModelDto
{
    public class ShowAbsenceByIdDto
    {
        public string UserCode { get; set; }

        public int AbsenceId { get; set; }
    }
}
