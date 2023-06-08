using VoteApp.Database;
using VoteApp.Database.User;
using VoteApp.Models.API.User;

namespace VoteApp.Host.Service.User;

public class UserService : IUserService
{
    private readonly IDatabaseContainer _databaseContainer;

    public UserService(IDatabaseContainer databaseContainer)
    {
        _databaseContainer = databaseContainer;
    }

    
    public async Task<UserModel> Create(RequestRegisterUser requestRegisterUser)
    {
       return await _databaseContainer.User.CreateUser(
            requestRegisterUser.Login,
            requestRegisterUser.FirstName,
            requestRegisterUser.LastName,
            requestRegisterUser.Phone,
            requestRegisterUser.Password);
    }


    public async Task<UserModel> GetOneById(int userId)
    {
        return await _databaseContainer.User.GetOneById(userId);
    }
}