using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.ModelDtos
{
    public class ChangePhoneNumberDto
    {
        public string PhoneNumber { get; set; }

        public string ConfirmPhoneNumber { get; set; }

        public string? Token { get; set; }  

    }
}
