using System.Net;
using VoteApp.API.Models.User;
using VoteApp.API.Test.Services;
using VoteApp.API.Test.Services.HttpService;
using Xunit;

namespace VoteApp.API.Test
{
    public class UserTest : IClassFixture<ClientFixture>
    {
        private readonly ICookieService _cookieService;
        private readonly IHttpService _httpService;

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
                Login = "john_test",
                FirstName = "john",
                LastName = "doe",
                Phone = "+123",
                Password = "123456"
            };

            var response = await _httpService.PostAsync<RegisterUser.Response, RegisterUser>("/api/User/Register", requestBody);
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
            Assert.Equal(response.Response.Login, requestBody.Login);
            Assert.Equal(response.Response.FirstName, requestBody.FirstName);
            Assert.Equal(response.Response.LastName, requestBody.LastName);
            Assert.Equal(response.Response.Phone, requestBody.Phone);
        }

        [Fact]
        public async Task User_Login_ReturnsOk()
        {
            var requestBody = new LoginUser()
            {
                Login = "John_test", 
                Password = "123456"
            };
            
            var response = await _httpService.PostAsync<LoginUser.Response, LoginUser>("/api/User/Login", requestBody);
            
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
            
            var cookieHeader = await _cookieService.GetCookieByLogin(user);
                
            var response = await _httpService.PostAsyncWithCredentials<LoginUser.Response, LoginUser>("/api/Candidate/CreateEmpty", cookieHeader, user);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}