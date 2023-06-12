using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace VoteApp.Database.User;

public class UserRepository : AbstractRepository<UserModel>, IUserRepository
{
    public UserRepository(PostgresContext context, ILoggerFactory loggerFactory) : base(context, loggerFactory) { }

    public async Task<UserModel> CreateUser(string login, string firstName, string lastName, string phone, string password)
    {
        var model = UserModel.Create(login, firstName, lastName, phone, password);
        
        var result = await CreateModelAsync(model);
        
        if(model is null)
        {
            throw new ArgumentException("User is not created. Instantiate error");
        }
            
        return result;
    }


    public async Task<UserModel> GetOneById(int id)
    {
        var model = await DbModel
            .Include(x=>x.Documents)
            .Include(x=>x.Candidates)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        if (model is null)
        {
            throw new ArgumentException($"User with Id: {id} is not found");
        }

        return model;
    }
    
    public async Task<UserModel> FindOneByLogin(string login)
    {
        var model = await DbModel
            .Include(x=>x.Documents)
            .Include(x=>x.Candidates)
            .Where(x => x.Login == login)
            .FirstOrDefaultAsync();

        if (model is null)
        {
            throw new ArgumentException($"User with login: {login} is not found");
        }

        return model;
    }
    
    public async Task<UserModel> GetOneByPhone(string phone)
    {
        var model = await DbModel.Where(x => x.Phone == phone).FirstOrDefaultAsync();

        if (model is null)
        {
            throw new ArgumentException($"User with phone: {phone} is not found");
        }

        return model;
    }
}