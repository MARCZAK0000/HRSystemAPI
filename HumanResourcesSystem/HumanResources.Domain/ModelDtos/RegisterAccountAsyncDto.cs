﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.ModelDtos
{
    public class RegisterAccountAsyncDto
    {
        public string Email {  get; set; } 

        public string Password { get; set; }    

        public string ConfirmPassword { get; set; } 

        public string PhoneNumber { get; set; } 
    }
}
