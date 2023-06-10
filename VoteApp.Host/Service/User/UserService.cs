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

    
    public async Task<UserModel> Create(RegisterUser registerUser)
    {
       return await _databaseContainer.User.CreateUser(
            registerUser.Login,
            registerUser.FirstName,
            registerUser.LastName,
            registerUser.Phone,
            registerUser.Password);
    }


    public async Task<UserModel> GetOneById(int userId)
    {
        return await _databaseContainer.User.GetOneById(userId);
    }
}