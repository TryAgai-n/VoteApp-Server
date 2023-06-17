using System.ComponentModel.DataAnnotations;

namespace VoteApp.API.Models.Candidate;

public class CandidateList
{
    public class Response
    {
        public Response(int id, string description, int? previewDocumentId)
        {
            Id = id;
            Description = description;
            PreviewDocumentId = previewDocumentId;
        }
        
        [Required]
        public int Id { get; }
        [Required]
        public string Description { get; }
        public int? PreviewDocumentId { get; }
    }
}