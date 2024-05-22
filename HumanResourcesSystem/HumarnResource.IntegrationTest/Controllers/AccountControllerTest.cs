using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Text;

namespace HumarnResource.IntegrationTest.Controllers
{
    public class AccountControllerTest
    { 

        [Fact]
        public async Task SignInUser_ShouldBeOK()
        {
            var factory = new TestFactory();
            using var stringContest = new StringContent(JsonSerializer.Serialize(new
            {
                email = "jj.marczak98@gmail.com",
                password = "Qwerty@69"
            }), 
            Encoding.UTF8,
            "application/json");

            var client = factory.CreateClient();
            var response = await client.PostAsync("/api/account/signin", stringContest);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task SignInUser_ShouldBeWrong()
        {
            var factory = new WebApplicationFactory<Program>();
            var client = factory.CreateClient();

            using var stringContest = new StringContent(JsonSerializer.Serialize(new
            {
                Email = "jj.marczak98@gmail.com",
                Password = "abc"
            }),
            Encoding.UTF8,
            "application/json");

            var response = await client.PostAsync("/api/account/signin", stringContest);


            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
