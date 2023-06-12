using VoteApp.Database.Candidate;
using VoteApp.Database.CandidateDocument;

namespace VoteApp.Host.Service.Candidate;

public interface ICandidateService
{
    Task<CandidateModel> GetOneById(int id);
    Task<CandidateModel> CreateEmpty(int userId);
    Task<CandidateDocumentModel> CreateCandidateDocument(int candidateId, int uploadPhotoId);
    Task <List<CandidateModel>> ListCandidateByStatus(CandidateStatus status, int skip, int take);
    Task<bool> IsUsersCandidate(int userId, CandidateModel candidate);

}