using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace HumarnResource.IntegrationTest.Controllers
{
    public class UserControllerTest:IClassFixture<WebApplicationFactory<Program>>
    {

        readonly HttpClient client;
        public UserControllerTest(WebApplicationFactory<Program> webApplication)
        {
            client = TestWebFactory.ReturnClient(webApplication);
        }

        [Fact]
        public async Task UpdateInformations_ShouldReturnOk()
        {
            var result = await client.GetAsync("/api/user/info");

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

    }
}
