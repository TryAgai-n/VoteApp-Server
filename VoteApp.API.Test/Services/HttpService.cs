using System.Text;
using Newtonsoft.Json;
using VoteApp.API.Models.User;

namespace VoteApp.API.Test.Services
{
    public class HttpService
    {
        private readonly HttpClient _httpClient;
        private readonly CookieService _cookieService;

        public HttpService(HttpClient httpClient, CookieService cookieService)
        {
            _httpClient = httpClient;
            _cookieService = cookieService;
        }

        public async Task<HttpResponseMessage> GetAsyncWithCredentials(LoginUser user, string url)
        {
            var cookieHeader = await _cookieService.GetCookieHeader(user);

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Cookie", cookieHeader);

            return await _httpClient.SendAsync(request);
        }

        public async Task<HttpResponseMessage> PostAsyncWithCredentials<T>(string url, string cookieHeader, T jsonContent)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("Cookie", cookieHeader);
            SetJsonContent(request, jsonContent);

            return await _httpClient.SendAsync(request);
        }
        
        public async Task<HttpResponseMessage> PostAsync<T>(string url, T jsonContent)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            SetJsonContent(request, jsonContent);

            return await _httpClient.SendAsync(request);
        }

        public async Task<HttpResponseMessage> PutAsyncWithCredentials<T>(string url, string cookieHeader, T jsonContent)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Headers.Add("Cookie", cookieHeader);
            SetJsonContent(request, jsonContent);

            return await _httpClient.SendAsync(request);
        }

        public async Task<HttpResponseMessage> DeleteAsyncWithCredentials(string url, string cookieHeader)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            request.Headers.Add("Cookie", cookieHeader);

            return await _httpClient.SendAsync(request);
        }

        private static void SetJsonContent<T>(HttpRequestMessage request, T jsonContent)
        {
            var json = JsonConvert.SerializeObject(jsonContent);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
