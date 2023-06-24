using System.Text;
using Newtonsoft.Json;
using Xunit.Sdk;

namespace VoteApp.API.Test.Services.HttpService
{

    public partial class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        
        public async Task<ResponseResult<T>> PostAsync<T, TJ>(string url, TJ jsonContent)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            SetJson(request, jsonContent);

            var response = await _httpClient.SendAsync(request);

            var result = new ResponseResult<T>
            {
                StatusCode = response.StatusCode,
                Response = await DeserializeAsync<T>(response)
            };

            return result;
        }

        
        public async Task<ResponseResult<T>>  PostAsyncWithCredentials<T, TJ>(string url, string cookieHeader, TJ jsonContent)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("Cookie", cookieHeader);
            SetJson(request, jsonContent);

            var response = await _httpClient.SendAsync(request);

            var result = new ResponseResult<T>
            {
                StatusCode = response.StatusCode,
                Response = await DeserializeAsync<T>(response)
            };

            return result;
        }
        

        public async Task<ResponseResult<T>>  GetAsyncWithCredentials<T>(string url, string cookieHeader)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Cookie", cookieHeader);

            var response = await _httpClient.SendAsync(request);

            var result = new ResponseResult<T>
            {
                StatusCode = response.StatusCode,
                Response = await DeserializeAsync<T>(response)
            };

            return result;
        }
        
        public async Task<ResponseResult<T>> PutAsyncWithCredentials<T>(string url, string cookieHeader, string jsonContent)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Headers.Add("Cookie", cookieHeader); 
            SetJson(request, jsonContent);

            var response = await _httpClient.SendAsync(request);

            var result = new ResponseResult<T>
            {
                StatusCode = response.StatusCode,
                Response = await DeserializeAsync<T>(response)
            };

            return result;
        }

        
        public async Task<ResponseResult<T>> DeleteAsyncWithCredentials<T>(string url, string cookieHeader)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            request.Headers.Add("Cookie", cookieHeader);

            var response = await _httpClient.SendAsync(request);

            var result = new ResponseResult<T>
            {
                StatusCode = response.StatusCode,
                Response = await DeserializeAsync<T>(response)
            };

            return result;
        }

      
        
        private static void SetJson<T>(HttpRequestMessage request, T jsonContent)
        {
            var json = JsonConvert.SerializeObject(jsonContent);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        private static async Task<T?> DeserializeAsync<T>(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(json);

            if (result == null)
            {
                throw new NullException($"Error: Deserialize Object from Response to {typeof(T)}");
            }
            
            return result;
        }
    }
}
