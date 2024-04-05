using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.EmailModelDto
{
    public class SendEmailDto
    {
        public string EmailFrom { get; set; }

        public string EmailTo { get; set; }

        public string EmailSubject { get; set; }

        public string EmailBody { get; set; }   
    }
}
