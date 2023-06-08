using VoteApp.Database.User;
using VoteApp.Models.API.User;

namespace VoteApp.Host.Utils.UserUtils;

public interface IUserUtils
{
    Task<int> GetUserIdFromValidCookies(HttpContext httpContext);

    Task<UserModel> ValidateUser(RequestLoginUser request);
}