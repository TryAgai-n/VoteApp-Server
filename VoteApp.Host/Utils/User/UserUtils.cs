using VoteApp.Database;
using VoteApp.Database.User;
using VoteApp.Models.API.User;

namespace VoteApp.Host.Utils.User;

public class UserUtils : IUserUtils
{
    private readonly IDatabaseContainer _databaseContainer;

    public UserUtils(IDatabaseContainer databaseContainer)
    {
        _databaseContainer = databaseContainer;
    }


    public Task<int> GetUserIdFromCookies(HttpContext httpContext)
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
}