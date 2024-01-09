using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.ModelDtos
{
    public class ChangePasswordAsyncDto
    { 
        public string Password { get; set; }

        public string ConfirmPassword {  get; set; }   
        
        public string NewPassword {  get; set; }

        public string ConfirmNewPassword { get; set;}
    }
}
