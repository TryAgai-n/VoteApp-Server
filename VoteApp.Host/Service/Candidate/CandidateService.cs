using VoteApp.Database;
using VoteApp.Database.Candidate;
using VoteApp.Models.API.Candidate;

namespace VoteApp.Host.Service.Candidate;

public class CandidateService : ICandidateService
{
    private readonly IDatabaseContainer _databaseContainer;

    public CandidateService(IDatabaseContainer databaseContainer)
    {
        _databaseContainer = databaseContainer;
    }


    public async Task<CandidateModel> GetOneById(int id, int userId)
    {
        var candidate = await _databaseContainer.Candidate.GetOneById(id);

        if (candidate.UserId != userId)
        {
            throw new UnauthorizedAccessException($"User {userId} access denied for document id {id}");
        }

        return candidate;
    }


    public async Task<CandidateModel> Create(string description, int userId)
    {
        return await _databaseContainer.Candidate.CreateCandidate(description, userId);
    }
}