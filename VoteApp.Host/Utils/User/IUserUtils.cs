
namespace VoteApp.Host.Utils.User;

public interface IUserUtils
{
    Task<int> GetUserIdFromCookies(HttpContext httpContext);
    
}