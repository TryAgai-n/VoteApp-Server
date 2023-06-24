using VoteApp.API.Models.User;

namespace VoteApp.API.Test.Services;

public interface ICookieService
{
    Task<string> GetCookieByLogin(LoginUser user);
}