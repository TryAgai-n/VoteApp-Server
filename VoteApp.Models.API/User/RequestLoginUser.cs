using System.ComponentModel.DataAnnotations;

namespace VoteApp.Models.API.User;

public class RequestLoginUser
{
    [Required]
    public string Login { get; set; }
    [Required]
    public string Password { get; set; }

}