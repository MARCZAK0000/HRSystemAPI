using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.EmailModelDto
{
    public class ConfirmEmailMessageInfoDto
    {
        public string UserName { get; set; }

        public string token {  get; set; }  
    }
}
