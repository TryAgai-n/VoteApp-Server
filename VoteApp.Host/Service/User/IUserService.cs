using VoteApp.Database.User;
using VoteApp.Models.API.User;

namespace VoteApp.Host.Service.User;

public interface IUserService
{


    Task<UserModel> Create(RequestRegisterUser requestRegisterUser);
    
    Task<UserModel> GetOneById(int userId);
    
}