using System.Net;

namespace VoteApp.API.Test.Services.HttpService;

public class ResponseResult<T>
{
    public T Response { get; set; }

    public HttpStatusCode StatusCode { get; set; }
}