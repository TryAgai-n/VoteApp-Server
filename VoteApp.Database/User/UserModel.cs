using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VoteApp.Database.Candidate;
using VoteApp.Database.Document;

namespace VoteApp.Database.User;

public class UserModel : AbstractModel
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public UserRole UserRole { get; set; }
    public List<DocumentModel> Documents { get; set; }
    public List<CandidateModel> Candidates { get; set; }



    public static UserModel Create(string login, string firstName, string lastName, string phone, string password)
    {
        return new UserModel
        {
            Login = login,
            FirstName = firstName,
            LastName = lastName,
            Phone = phone,
            Password = password,
            UserRole = UserRole.User,
        };
    }
}