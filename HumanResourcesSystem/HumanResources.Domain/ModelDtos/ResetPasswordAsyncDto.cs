using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.ModelDtos
{
    public class ResetPasswordAsyncDto
    {
        public string Email { get; set; }   

        public string Password { get; set; }

        public string ConfirmPassword { get; set; } 

        public string Token { get; set; }   


    }
}
