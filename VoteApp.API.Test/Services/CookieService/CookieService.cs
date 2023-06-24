using System.Text;
using Newtonsoft.Json;
using VoteApp.API.Models.User;

namespace VoteApp.API.Test.Services;

public class CookieService : ICookieService
{
    private readonly HttpClient _httpClient;
    
    public CookieService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<string> GetCookieByLogin(LoginUser user)
    {
        var requestBody = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
    
        var response = await _httpClient.PostAsync("/api/User/Login", requestBody);
    
        var cookieHeaders = response.Headers.GetValues("Set-Cookie");
        var cookieHeader = string.Join("; ", cookieHeaders);

        return cookieHeader;
    }
}