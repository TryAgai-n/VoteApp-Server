using VoteApp.Database.Candidate;
using VoteApp.Database.Document;

namespace VoteApp.Database.CandidateDocument;

public class CandidateDocumentModel : AbstractModel
{
    public int CandidateId { get; set; }
    public CandidateModel Candidate { get; set; }
    public int DocumentId { get; set; }
    public DocumentModel Document { get; set; }
    
    
    public static CandidateDocumentModel Create(int candidateId, int documentId)
    {
        return new CandidateDocumentModel
        {
            CandidateId = candidateId,
            DocumentId = documentId
        };
    }
}