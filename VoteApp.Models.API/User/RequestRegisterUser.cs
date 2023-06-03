using System.ComponentModel.DataAnnotations;

namespace VoteApp.Models.API.User;

public class RequestRegisterUser
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

}