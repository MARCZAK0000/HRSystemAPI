using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Infrastructure.Authentication
{
    public class AuthenticationSettings
    {
        public string JwtIssuer {  get; set; }  
        
        public string JwtKey { get; set; }  

        public int JwtExpireDay { get; set; }
    }
}
