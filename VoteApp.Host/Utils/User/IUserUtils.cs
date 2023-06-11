using VoteApp.Database.User;
using VoteApp.Models.API.User;

namespace VoteApp.Host.Utils.User;

public interface IUserUtils
{
    Task<int> GetUserIdFromCookies(HttpContext httpContext);

    Task<UserModel> ValidateUser(LoginUser request);

}