using System.Net;
using VoteApp.API.Models.User;
using VoteApp.API.Test.Services;
using Xunit;

namespace VoteApp.API.Test
{
    public class UserTest : IClassFixture<ClientFixture>
    {
        private readonly CookieService _cookieService;
        private readonly HttpService _httpService;


        public UserTest(ClientFixture fixture)
        {
            _httpService = fixture.HttpService;
            _cookieService = fixture.CookieService;
        }
        
        [Fact]
        public async Task User_Register_ReturnsOk()
        {
            var requestBody = new RegisterUser
            {
                Login = "John_test",
                FirstName = "John",
                LastName = "Doe",
                Phone = "+123",
                Password = "123456"
            };

            var response = await _httpService.PostAsync("/api/User/Register", requestBody);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task User_Login_ReturnsOk()
        {
            var requestBody = new LoginUser()
            {
                Login = "John_test", 
                Password = "123456"
            };

            var response = await _httpService.PostAsync("/api/User/Login", requestBody);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        [Fact]
        public async Task Candidate_CreateEmpty_ReturnsOk()
        {
            var user = new LoginUser()
            {
                Login = "Admin", 
                Password = "123"
            };

            var cookieHeader = await _cookieService.GetCookieHeader(user);
                
            var response = await _httpService.PostAsyncWithCredentials("/api/Candidate/CreateEmpty", cookieHeader, " ");
            
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}