using System;
using System.Collections.Generic;
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
        var model = await DbModel
            .Include(x => x.CandidateDocuments)
            .Include(x=>x.User.Documents)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        if (model is null)
        {
            throw new ArgumentException($"Candidate with Id: {id} is not found");
        }

        return model;
    }


    public async Task<CandidateModel> CreateEmptyCandidate(int userId)
    {
        var model = CandidateModel.CreateEmpty(userId);
        
        var result = await CreateModelAsync(model);
        
        if(model is null)
        {
            throw new Exception("Candidate is not created. Instantiate error");
        }
            
        return result;
    }


    public async Task<List<CandidateModel>> ListCandidateByStatus(CandidateStatus status, int skip, int take)
    {
        return await DbModel.Where(x => x.Status == status)
            .OrderBy(x => x.Id)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }

}