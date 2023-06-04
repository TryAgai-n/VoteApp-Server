using VoteApp.Database.User;
using VoteApp.Models.API.User;

namespace VoteApp.Host.Service;

public interface IUserService
{
    Task<int> GetUserIdFromValidCookies(HttpContext httpContext);

    Task<UserModel> Create(RequestRegisterUser requestRegisterUser);
}