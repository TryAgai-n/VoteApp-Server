using VoteApp.Database.User;
using VoteApp.Models.API.User;

namespace VoteApp.Host.Service.User;

public interface IUserService
{
    Task<int> GetUserIdFromValidCookies(HttpContext httpContext);

    Task<UserModel> ValidateUser(RequestLoginUser request);

    Task<UserModel> Create(RequestRegisterUser requestRegisterUser);
    
}