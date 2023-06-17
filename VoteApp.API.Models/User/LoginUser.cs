using System.ComponentModel.DataAnnotations;

namespace VoteApp.API.Models.User;

public class LoginUser
{
    [Required]
    public string Login { get; set; }
    [Required]
    public string Password { get; set; }
    
    
    public class Response
    {
        public Response()
        { }
    }

}