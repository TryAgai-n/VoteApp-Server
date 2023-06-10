using VoteApp.Database;
using VoteApp.Database.Candidate;
using VoteApp.Database.CandidateDocument;

namespace VoteApp.Host.Service.Candidate;

public class CandidateService : ICandidateService
{
    private readonly IDatabaseContainer _databaseContainer;

    public CandidateService(IDatabaseContainer databaseContainer)
    {
        _databaseContainer = databaseContainer;
    }


    public async Task<CandidateModel> GetOneById(int id)
    {
        var candidate = await _databaseContainer.Candidate.GetOneById(id);
        return candidate;
    }


    public async Task<CandidateModel> Create(string description, int userId)
    {
        return await _databaseContainer.Candidate.CreateCandidate(description, userId);
    }
    
    public async Task<CandidateDocumentModel> CreateCandidateDocument(int candidateId, int uploadPhotoId)
    {
        return  await _databaseContainer.CandidateDocument.Create(candidateId, uploadPhotoId);
    }


    public async Task<List<CandidateModel>> ListCandidateByStatus(CandidateStatus status, int skip, int take)
    {
        return await _databaseContainer.Candidate.ListCandidateByStatus(status, skip, take);
    }




    public async Task<bool> IsUsersCandidate(int userId, CandidateModel candidate)
    {
        if (candidate.UserId != userId)
        {
            throw new UnauthorizedAccessException($"User {userId} access denied for document id {candidate.UserId}");
        }
        return true;
    }
}