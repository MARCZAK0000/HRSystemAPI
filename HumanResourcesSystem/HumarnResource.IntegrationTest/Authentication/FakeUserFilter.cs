using Microsoft.AspNetCore.Mvc.Filters;
using Moq;
using System.Security.Claims;

namespace HumarnResource.IntegrationTest.Authentication
{
    public class FakeUserFilter:IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var identity = new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier , "100"),
                new Claim(ClaimTypes.Name, "test@example.com"),
                new Claim(ClaimTypes.Email,"test@example.com"),
                new Claim(ClaimTypes.SerialNumber,"00000000000"),
                new Claim(ClaimTypes.Role, "User")
            }, "custom");

            var claimsPrinciple = new ClaimsPrincipal(identity);

            

            context.HttpContext.User = claimsPrinciple;
            await next();
                
        }
    }
}
