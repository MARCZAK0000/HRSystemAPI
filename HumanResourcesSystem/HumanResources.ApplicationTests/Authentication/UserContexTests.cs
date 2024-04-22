using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using Xunit;

namespace HumanResources.Application.Authentication.Tests
{
    public class UserContexTests
    {
        [Fact()]
        public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
        {
            //Arrange
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, "test@example.com"),
                new Claim(ClaimTypes.Email,"test@example.com"),
                new Claim(ClaimTypes.SerialNumber,"00000000000"),
                new Claim(ClaimTypes.Role, "Leader")
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();


            httpContextAccessorMock.Setup(x=> x.HttpContext).Returns(new DefaultHttpContext()
            {
                User = user,
                
            });

            
            var userContext = new UserContex(httpContextAccessorMock.Object);
            //Act

            var currentUser = userContext.GetCurrentUser();

            currentUser.Should().NotBeNull();
            currentUser.Id.Should().Be("1");
            currentUser.UserCode.Should().Be("00000000000");
        }
    }
}