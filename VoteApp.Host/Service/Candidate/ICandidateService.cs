using VoteApp.Database.Candidate;
using VoteApp.Models.API.Candidate;

namespace VoteApp.Host.Service.Candidate;

public interface ICandidateService
{
    Task<CandidateModel> GetOneById(int id, int userId);
    Task<CandidateModel> Create(string description, int userId);
}