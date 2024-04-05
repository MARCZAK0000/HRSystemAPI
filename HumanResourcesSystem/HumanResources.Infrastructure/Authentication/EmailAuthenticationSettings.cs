using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Infrastructure.Authentication
{
    public class EmailAuthenticationSettings
    {
        public string Host {get; set;}
        
        public int Port { get; set;}

        public string Email { get; set;}

        public string Password { get; set;}


    }
}
