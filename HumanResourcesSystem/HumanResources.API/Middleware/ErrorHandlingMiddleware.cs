using HumanResources.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace HumanResources.API.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(BadRequestException ex)
            {
                _logger.LogError(ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                var problemDetails = new ProblemDetails()
                {
                    Title = "BadRequest",
                    Type = "Client Error",
                    Status = (int)HttpStatusCode.BadRequest,
                    Detail = ex.Message
                };
                var json = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(json);
            }
            catch (InvalidEmailOrPasswordExcepiton ex)
            {
                _logger.LogError(ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.ContentType = "application/json";
                var problemDetails = new ProblemDetails()
                {
                    Title = "InvalidEmailOrPassword",
                    Type = "Client Error",
                    Status = (int)HttpStatusCode.NotFound,
                    Detail = ex.Message
                };
                var json = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(json);
            }

            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.ContentType = "application/json";
                var problemDetails = new ProblemDetails()
                {
                    Title = "NotFound",
                    Type = "Client Error",
                    Status = (int)HttpStatusCode.NotFound,
                    Detail = ex.Message
                };
                var json = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(json);
            }

            catch (ForbidenException ex)
            {
                _logger.LogError(ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                context.Response.ContentType = "application/json";
                var problemDetails = new ProblemDetails()
                {
                    Title = "Forbidden",
                    Type = "Client Error",
                    Status = (int)HttpStatusCode.Forbidden,
                    Detail = ex.Message
                };
                var json = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(json);
            }

            catch (UnauthorizedExceptions ex)
            {
                _logger.LogError(ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.ContentType = "application/json";
                var problemDetails = new ProblemDetails()
                {
                    Title = "UnAuthorized",
                    Type = "Client Error",
                    Status = (int)HttpStatusCode.Unauthorized,
                    Detail = ex.Message
                };
                var json = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(json);
            }
            catch (SavingChangesToDatabaseException ex)
            {
                _logger.LogError(ex.ToString());
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var problemDetails = new ProblemDetails()
                {
                    Title = "Internal Server Error - database Error",
                    Type = "Server Error",
                    Status = (int)HttpStatusCode.InternalServerError,
                    Detail = ex.Message
                };
                var response = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(response);
            }

            catch (ServerErrorException ex)
            {
                _logger.LogError(ex.ToString());
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var problemDetails = new ProblemDetails()
                {
                    Title = "Internal Server Error - Server Error",
                    Type = "Server Error",
                    Status = (int)HttpStatusCode.InternalServerError,
                    Detail = ex.Message
                };
                var response = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(response);
            }

            catch (Exception ex)//Error 500
            {
                _logger.LogError(ex.ToString());
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var problemDetails = new ProblemDetails()
                {
                    Title = "Internal Server Error",
                    Type = "Server Error",
                    Status = (int)HttpStatusCode.InternalServerError,
                    Detail = ex.Message
                };
                var response = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(response);
            }
        }
    }
}
