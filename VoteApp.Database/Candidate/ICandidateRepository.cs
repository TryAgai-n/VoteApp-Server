using System.Collections.Generic;
using System.Threading.Tasks;

namespace VoteApp.Database.Candidate;

public interface ICandidateRepository
{
    Task<CandidateModel> GetOneById(int id);
    Task<CandidateModel> CreateCandidate(string description, int userId);
    
    Task <List<CandidateModel>> ListCandidateByStatus(CandidateStatus status, int skip, int take);
}