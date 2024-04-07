using HumanResources.Application.ServiceLogger;
using System.Text;
namespace HumanResources.API.Middleware
{
    public class LoggerMiddleware : IMiddleware
    {
        
        private readonly ILogger<LoggerMiddleware> _logger;

        private readonly ILoggerService _loggerService;
        public LoggerMiddleware(ILogger<LoggerMiddleware> logger, ILoggerService loggerService)
        {
            _logger = logger;
            _loggerService = loggerService;
        }


        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {   

            try
            {
                await next.Invoke(context);
            }
            finally
            {
                _logger.LogInformation(_loggerService.LogRequest(context));
                _logger.LogInformation(_loggerService.LogResponse(context));
            }
        }


         
    }
}
