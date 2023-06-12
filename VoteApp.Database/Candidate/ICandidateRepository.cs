using System.Collections.Generic;
using System.Threading.Tasks;

namespace VoteApp.Database.Candidate;

public interface ICandidateRepository
{
    Task<CandidateModel> GetOneById(int id);
    Task<CandidateModel> CreateEmptyCandidate(int userId);
    
    Task <List<CandidateModel>> ListCandidateByStatus(CandidateStatus status, int skip, int take);
}