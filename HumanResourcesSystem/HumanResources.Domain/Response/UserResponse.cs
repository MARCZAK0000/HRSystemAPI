﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.Response
{
    public class UserResponse
    {
        public bool Result { get; set; }    

        public string? Username { get; set; }

        public string? Email { get; set; }  

        public string? Token { get; set; }
        
        public string? Message { get; set; }    
    }
}
