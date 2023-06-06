using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace VoteApp.Database.Candidate;

public class CandidateRepository : AbstractRepository<CandidateModel>, ICandidateRepository
{
    public CandidateRepository(PostgresContext context, ILoggerFactory loggerFactory) : base(context, loggerFactory) { }

    
    
    public async Task<CandidateModel> GetOneById(int id)
    {
        var model = await DbModel.Where(x => x.Id == id).FirstOrDefaultAsync();

        if (model is null)
        {
            throw new ArgumentException($"Candidate with Id: {id} is not found");
        }

        return model;
    }


    public async Task<CandidateModel> CreateCandidate(string description, int userId)
    {
        var model = CandidateModel.Create(description, userId);
        
        var result = await CreateModelAsync(model);
        
        if(model is null)
        {
            throw new Exception("Candidate is not created. Instantiate error");
        }
            
        return result;
    }
    
}