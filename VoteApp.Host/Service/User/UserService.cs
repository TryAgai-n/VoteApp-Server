using System.Security.Cryptography;
using System.Text;
using VoteApp.Database;
using VoteApp.Database.User;
using VoteApp.API.Models.User;

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
        var toLowerLogin = registerUser.Login.ToLower();

        var hashedPassword = await HashPassword(registerUser.Password);
        
        return await _databaseContainer.User.CreateUser(
            toLowerLogin,
            registerUser.FirstName,
            registerUser.LastName,
            registerUser.Phone,
            hashedPassword
        );
    }
    
    public async Task<UserModel> ValidateUser(LoginUser request)
    {
        var toLowerLogin = request.Login.ToLower();
    
        var user = await _databaseContainer.User.FindOneByLogin(toLowerLogin);

        var hashedPassword = await HashPassword(request.Password);

        if (user.Password != hashedPassword)
        {
            throw new ArgumentException("Login or password is wrong");
        }

        return user;
    }
    
    private async Task<string> HashPassword(string password)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);

        var hashedBytes = await Task.Run(() => SHA256.HashData(passwordBytes));

        var hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

        return hashedPassword;
    }
    
    public async Task<UserModel> GetOneById(int userId)
    {
        return await _databaseContainer.User.GetOneById(userId);
    }
}