using System.ComponentModel.DataAnnotations;

namespace VoteApp.Models.API.Candidate;

public class CandidateList
{
    public class Response
    {
        public Response(int id, string description, int previewDocumentId)
        {
            Id = previewDocumentId;
            Description = description;
            PreviewDocumentId = previewDocumentId;
        }
        
        [Required]
        public int Id { get; }
        [Required]
        public string Description { get; }
        [Required]
        public int PreviewDocumentId { get; }
    }
}