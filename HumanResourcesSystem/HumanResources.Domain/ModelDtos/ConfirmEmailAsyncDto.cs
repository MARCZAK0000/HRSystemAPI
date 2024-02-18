using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.ModelDtos
{
    public class ConfirmEmailAsyncDto
    {
        public string Email { get; set; } 
         
        public string Token { get; set; }  
    }
}
