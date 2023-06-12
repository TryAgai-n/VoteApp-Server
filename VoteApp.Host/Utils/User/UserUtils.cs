using VoteApp.Database.User;

namespace VoteApp.Host.Utils.User;

public class UserUtils : IUserUtils
{
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