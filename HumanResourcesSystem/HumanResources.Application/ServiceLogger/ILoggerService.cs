using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Application.ServiceLogger
{
    public interface ILoggerService
    {
        string LogRequest(HttpContext context);

        string LogResponse(HttpContext context);

        
    }
}
