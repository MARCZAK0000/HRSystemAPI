using FluentAssertions;
using HumarnResource.IntegrationTest.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using System.Text.Json;

namespace HumarnResource.IntegrationTest.Controllers
{
    public class Controlles_Test:IClassFixture<WebApplicationFactoryBuilder>
    {
        private readonly WebApplicationFactoryBuilder _factory; 
        
        public Controlles_Test(WebApplicationFactoryBuilder factory)
        {
            _factory = factory;
        }
        [Theory]
        [ClassData(typeof(RegisterUserData))]
        public async Task Register_ShouldBeOk(string Email, string Password, string ConfirmPassword, string Phone)
        {

            
            using var stringContest = new StringContent(JsonSerializer.Serialize(new
            {

                email = Email,
                password = Password,
                confirmPassword = ConfirmPassword,
                phoneNumber = Phone
            }),
            Encoding.UTF8,
            "application/json");
            
            var response = await _factory._client.PostAsync("/api/account/register", stringContest);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }

        [Theory]
        [ClassData(typeof(SignInUserData))]
        public async Task SignInUser_ShouldBeOK(string Email, string Password)
        {
            using var stringContest = new StringContent(JsonSerializer.Serialize(new
            {
                email = Email,
                password = Password
            }),
            Encoding.UTF8,
            "application/json");

            var response = await _factory._client.PostAsync("/api/account/signin", stringContest);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task AccountController_SignInUser_ShouldBeWrong()
        {

            using var stringContest = new StringContent(JsonSerializer.Serialize(new
            {
                Email = "jj.marczak98@gmail.com",
                Password = "abc"
            }),
            Encoding.UTF8,
            "application/json");

            var response = await _factory._client.PostAsync("/api/account/signin", stringContest);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task AccountController_RefreshToken_ResultShouldBeOK()
        {
            using var stringContest = new StringContent(JsonSerializer.Serialize(new
            {
                RefreshToken = "0000"
            }),
             Encoding.UTF8,
            "application/json");

            var response = await _factory._client.PostAsync("/api/account/refresh", stringContest);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        }

        [Fact]
        public async Task UserController_GetUser_Result_ShouldBeOk()
        {
            var result = await _factory._client.GetAsync("/api/user/info");

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
