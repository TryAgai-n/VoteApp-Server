using System.ComponentModel.DataAnnotations;

namespace VoteApp.Models.API.User;

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