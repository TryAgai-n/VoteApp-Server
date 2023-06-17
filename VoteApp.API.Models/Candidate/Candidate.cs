namespace VoteApp.API.Models.Candidate;

public class Candidate
{
    public class Response
    {
        public Response(int id, string description, int? previewDocumentId, int userId, List<int> documentIds)
        {
            Id = id;
            Description = description;
            PreviewDocumentId = previewDocumentId;
            UserId = userId;
            DocumentIds = documentIds;
        }
        
        public int Id { get; set; }
        public string Description { get; set; }
        public int? PreviewDocumentId { get; set; }
        public int UserId { get; set; }
        public List<int> DocumentIds { get; set; }
    }
}

