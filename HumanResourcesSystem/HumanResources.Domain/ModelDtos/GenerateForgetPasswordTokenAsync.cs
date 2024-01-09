using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.ModelDtos
{
    public class GenerateForgetPasswordTokenAsync
    {
        public string Email {  get; set; }  

        public string PhoneNumber { get; set; } 
    }
}
