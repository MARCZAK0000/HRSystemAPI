﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.Exceptions
{
    public class ServerErrorException : Exception
    {
        public ServerErrorException(string? message) : base(message)
        {
        }
    }
}
