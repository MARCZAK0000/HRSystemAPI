using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Application.ServiceLogger
{
    public class LoggerService : ILoggerService
    {
        

        public string LogRequest(HttpContext context)
        {
            var text = new StringBuilder();

            text.AppendLine("Incoming...");
            text.AppendLine($"HTTP {context.Request.Method} {context.Request.Path} ");
            text.AppendLine($"Host: {context.Request.Host} ");
            text.AppendLine($"Content-Type: {context.Request.ContentType} ");
            text.AppendLine($"Content-Length: {context.Request.ContentLength} ");

            return text.ToString();
        }

        public string LogResponse(HttpContext context)
        {
            var text = new StringBuilder();

            text.AppendLine("Incoming...");
            text.AppendLine("Outgoing Response:");
            text.AppendLine($"HTTP {context.Response.StatusCode}");
            text.AppendLine($"Content-Type: {context.Response.ContentType}");
            text.AppendLine($"Content-Length: {context.Response.ContentLength}"); ;

            return text.ToString();
        }
    }
}
