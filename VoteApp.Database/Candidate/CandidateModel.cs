using System.Collections.Generic;
using VoteApp.Database.CandidateDocument;
using VoteApp.Database.User;

namespace VoteApp.Database.Candidate;

public class CandidateModel
{
    public int Id { get; set; }
    
    public string Description { get; set; }
    
    public int? PreviewDocumentId { get; set; }
    public int UserId { get; set; }
    public UserModel User { get; set; }
    public List<CandidateDocumentModel> CandidateDocuments { get; set; }
}