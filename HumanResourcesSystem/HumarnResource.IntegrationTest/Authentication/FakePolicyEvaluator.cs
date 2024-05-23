using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using System.Security.Claims;

namespace HumarnResource.IntegrationTest.Authentication
{
    public class FakePolicyEvaluator : IPolicyEvaluator
    {
        public Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
        {
            var principle = new ClaimsPrincipal();
            var ticket = new AuthenticationTicket(principle, "Bearer");
            var result = AuthenticateResult.Success(ticket);

            return Task.FromResult(result);
        }

        public Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy, AuthenticateResult authenticationResult, HttpContext context, object? resource)
        {
            return Task.FromResult(PolicyAuthorizationResult.Success());
        }
    }
}
