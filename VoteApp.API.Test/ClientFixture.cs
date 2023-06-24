using System.Net;
using VoteApp.API.Test.Services;
using VoteApp.API.Test.Services.HttpService;

namespace VoteApp.API.Test
{
    public class ClientFixture
    {
        public HttpClient HttpClient { get; }
        public IHttpService HttpService { get; }
        public ICookieService CookieService { get; }
        public CookieContainer CookieContainer { get; }
        
        public ClientFixture()
        {
            CookieContainer = new CookieContainer();
            
            HttpClient = new HttpClient(new HttpClientHandler
            {
                CookieContainer = CookieContainer,
                UseCookies = true,
                UseDefaultCredentials = false
            })
            {
                BaseAddress = new Uri("http://localhost:9999")
            };

            CookieService = new CookieService(HttpClient);
            HttpService = new HttpService(HttpClient);
        }
    }
}