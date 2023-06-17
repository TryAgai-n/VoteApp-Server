using System.ComponentModel.DataAnnotations;

namespace VoteApp.API.Models.User;

public class RegisterUser
{
    [Required]
    public string Login { get; set; }

    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }

    [Required]
    public string Phone { get; set; }

    [Required]
    public string Password { get; set; }

    
    public class Response
    {
        public Response(string login, string firstName, string lastName, string phone)
        {
            Login = login;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
        }
        
        [Required]
        public string Login { get; set; }

        [Required]
        public string FirstName { get; set; }
    
        [Required]
        public string LastName { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}