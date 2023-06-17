using System.ComponentModel.DataAnnotations;
using VoteApp.Database.Candidate;
using VoteApp.Database.Document;
using VoteApp.Database.User;

namespace VoteApp.API.Models.User;

public class FullUserInfo
{
    [Required]
    public int Id { get; set; }
    
    public class Response
    {
        public Response(
            string login,
            string firstName,
            string lastName, 
            string phone,
            UserRole userRole,
            List<int> documentsIds,
            List<int> candidateIds)
        {
            Login = login;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            UserRole = userRole;
            DocumentIds = documentsIds;
            CandidateIds = candidateIds;
        }
        
        [Required]
        public string Login { get; set; }

        [Required]
        public string FirstName { get; set; }
    
        [Required]
        public string LastName { get; set; }

        [Required]
        public string Phone { get; set; }
        
        [Required]
        public UserRole UserRole { get; set; }
        
        [Required]
        public List<int> DocumentIds { get; set; }
        
        [Required]
        public List<int> CandidateIds { get; set; }
    }
}