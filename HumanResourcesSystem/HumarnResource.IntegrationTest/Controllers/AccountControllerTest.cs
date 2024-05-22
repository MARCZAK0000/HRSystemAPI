using FluentAssertions;
using HumarnResource.IntegrationTest.Data;
using System.Text;
using System.Text.Json;

namespace HumarnResource.IntegrationTest.Controllers
{
    public class AccountControllerTest
    {

        readonly TestFactory factory;
        HttpClient client;
        public AccountControllerTest()
        {
            factory = new TestFactory();
            client = factory.CreateClient();
               
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

            var response = await client.PostAsync("/api/account/register", stringContest);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }

        [Theory]
        [ClassData(typeof(SignInUserData))]
        public async Task SignInUser_ShouldBeOK(string Email, string Phone)
        {
            using var stringContest = new StringContent(JsonSerializer.Serialize(new
            {
                email = Email,
                password = Phone
            }),
            Encoding.UTF8,
            "application/json");

            var response = await client.PostAsync("/api/account/signin", stringContest);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task SignInUser_ShouldBeWrong()
        {

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

        [Fact]
        public async Task RefreshToken_ResultShouldBeOK()
        {
            using var stringContest = new StringContent(JsonSerializer.Serialize(new
            {
                RefreshToken = "0000"
            }),
             Encoding.UTF8,
            "application/json");

            var response = await client.PostAsync("/api/account/refresh", stringContest);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        }
    }
}
