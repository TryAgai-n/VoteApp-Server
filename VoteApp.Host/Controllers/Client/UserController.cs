using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoteApp.Database;
using VoteApp.Database.User;
using VoteApp.Host.Service.User;
using VoteApp.Models.API.User;

namespace VoteApp.Host.Controllers.Client;


public class UserController : AbstractClientController
{
   private readonly IUserService _userService;
   
   public UserController(
      IDatabaseContainer databaseContainer,
      IUserService userService) : base(databaseContainer)
   {
      _userService = userService;
   }
   
   [AllowAnonymous]
   [HttpPost]
   public async Task<IActionResult> Register(RequestRegisterUser request)
   {
      if (!ModelState.IsValid)
      {
         return BadRequest();
      }
      
      var user = await _userService.Create(request);
      
      return Ok(user);
   }


   [AllowAnonymous]
   [HttpPost]
   public async Task<IActionResult> Login([FromBody] RequestLoginUser request)
   {
      if (!ModelState.IsValid)
      {
         return BadRequest();
      }

      var user = await _userService.ValidateUser(request);
 
      var claims = new[]
      {
         new Claim(UserClaims.Id.ToString(),   user.Id.ToString()),
         new Claim(UserClaims.Name.ToString(), user.FirstName),
         new Claim(UserClaims.Role.ToString(), user.UserRole.ToString())
      };
        
      var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
      var principal = new ClaimsPrincipal(identity);
        
      await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
      return Ok();
   }
}