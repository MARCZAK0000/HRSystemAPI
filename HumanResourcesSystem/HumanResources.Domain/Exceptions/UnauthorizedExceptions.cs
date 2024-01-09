using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.Exceptions
{
    public class UnauthorizedExceptions : Exception
    {
        public UnauthorizedExceptions(string? message) : base(message)
        {
        }
    }
}
