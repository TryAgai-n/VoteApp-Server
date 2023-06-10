using System.Collections.Generic;
using VoteApp.Database.CandidateDocument;
using VoteApp.Database.User;

namespace VoteApp.Database.Candidate;

public class CandidateModel : AbstractModel
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int PreviewDocumentId { get; set; }
    public CandidateStatus Status { get; set; }
    public int UserId { get; set; }
    public UserModel User { get; set; }
    public List<CandidateDocumentModel> CandidateDocuments { get; set; }

    public static CandidateModel Create(string description, int userId)
    {
        return new CandidateModel
        {
            Description = description,
            UserId = userId
        };
    }
}