using VoteApp.Database;
using VoteApp.Database.User;
using VoteApp.Models.API.User;

namespace VoteApp.Host.Utils.UserUtils;

public class UserUtils : IUserUtils
{
    private readonly IDatabaseContainer _databaseContainer;

    public UserUtils(IDatabaseContainer databaseContainer)
    {
        _databaseContainer = databaseContainer;
    }


    public Task<int> GetUserIdFromValidCookies(HttpContext httpContext)
    {
        var userIdClaim = httpContext.User.FindFirst(UserClaims.Id.ToString());
    
        if (userIdClaim == null)
        {
            throw new UnauthorizedAccessException();
        }

        if (!int.TryParse(userIdClaim.Value, out var userId))
        {
            throw new ArgumentException("User ID must be int");
        }

        return Task.FromResult(userId);
    }


    public async Task<UserModel> ValidateUser(RequestLoginUser request)
    {
        var user = await _databaseContainer.User.FindOneByLogin(request.Login);

        if (user.Password != request.Password)
        {
            throw new ArgumentException("Login or password is wrong");
        }

        return user;
    }
}