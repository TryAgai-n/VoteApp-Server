using System.Threading.Tasks;

namespace VoteApp.Database.CandidateDocument;

public interface ICandidateDocumentRepository
{
    public Task<CandidateDocumentModel> Create(int candidateId, int documentId);
}