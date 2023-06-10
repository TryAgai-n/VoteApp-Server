using Microsoft.AspNetCore.Mvc;
using VoteApp.Database;
using VoteApp.Models.API.User;

namespace VoteApp.Host.Controllers.Admin;

public class AdminTestController : AbstractAdminController
{
    
    public AdminTestController(IDatabaseContainer databaseContainer) : base(databaseContainer) { }
    
    [HttpGet]
    public async Task<IActionResult> UserById(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
      
        var response = await DatabaseContainer.User.GetOneById(id);
        return Ok(response);
    }
   

    [HttpPost]
    public async Task<IActionResult> CreateUser(RegisterUser requestUser)
    {
        var user = await DatabaseContainer.User.CreateUser(
            requestUser.Login,
            requestUser.FirstName,
            requestUser.LastName, 
            requestUser.Phone, 
            requestUser.Password);
        
        return Ok(user);
    }


}