using VoteApp.Database.User;
using VoteApp.Models.API.User;

namespace VoteApp.Host.Service.User;

public interface IUserService
{
    
    Task<UserModel> Create(RegisterUser registerUser);
    
    Task<UserModel> GetOneById(int userId);
    
    Task<UserModel> ValidateUser(LoginUser request);
    
}