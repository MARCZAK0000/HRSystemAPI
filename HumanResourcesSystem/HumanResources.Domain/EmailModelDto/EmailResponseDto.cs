using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.EmailModelDto
{
    public class EmailResponseDto
    {
        public string? EmailFrom { get; set; } 

        public string? Emailto { get; set; }

        public bool IsConfirmed { get; set; }

        public string? Message { get; set; }
    }
}
