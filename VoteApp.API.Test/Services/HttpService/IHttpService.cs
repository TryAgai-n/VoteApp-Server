namespace VoteApp.API.Test.Services.HttpService;

public interface IHttpService
{
    Task<ResponseResult<T>> PostAsync<T, TJ>(string url, TJ jsonContent);
    Task<ResponseResult<T>> PostAsyncWithCredentials<T, TJ>(string url, string cookieHeader, TJ jsonContent);
    Task<ResponseResult<T>> GetAsyncWithCredentials<T>(string url, string cookieHeader);
    Task<ResponseResult<T>> PutAsyncWithCredentials<T>(string url, string cookieHeader, string jsonContent);
    Task<ResponseResult<T>>  DeleteAsyncWithCredentials<T>(string url, string cookieHeader);
}