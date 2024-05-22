using System.Security.Claims;

namespace HumarnResource.IntegrationTest.Authentication
{
    public class Authentication
    {
        public void GetAuthentication(string email)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.SerialNumber, "00000000000"),
                new Claim(ClaimTypes.Role, "Leader")
            };


        }
    }
}
