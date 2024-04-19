using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.Response
{
    public class DepartmentResponse
    {
        public bool Result { get; set; }

        public string Message { get; set; }

        public int DepartmentId { get; set; }
    }
}
