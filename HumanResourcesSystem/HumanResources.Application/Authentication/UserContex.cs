using HumanResources.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Application.Authentication
{
    public class UserContex : IUserContext
    {
        private readonly IHttpContextAccessor _httpContext;

        public UserContex(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }


        public CurrentUser GetCurrentUser()
        {
            var user = _httpContext.HttpContext.User ?? throw new UnauthorizedExceptions("Unauthorized");

            if (user.Identity is null || !user.Identity.IsAuthenticated)
            {
                throw new UnauthorizedExceptions("Unauthorized");
            }

            var id = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var roles = user.Claims.Where(c=>c.Type==ClaimTypes.Role).Select(c=>c.Value).ToList();

            return new CurrentUser(id, roles);
            
        }         
    }
}
