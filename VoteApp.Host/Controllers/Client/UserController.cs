using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoteApp.Database.User;
using VoteApp.Host.Service;
using VoteApp.Host.Utils;
using VoteApp.API.Models.User;

namespace VoteApp.Host.Controllers.Client;


public class UserController : AbstractClientController
{
   public UserController(IServiceFactory serviceFactory, IUtilsFactory utilsFactory) 
      : base(serviceFactory, utilsFactory) { }

   
   [AllowAnonymous]
   [HttpPost]
   public async Task<IActionResult> Register(RegisterUser request)
   {
      if (!ModelState.IsValid)
      {
         return BadRequest();
      }
      
      var user = await ServiceFactory.UserService.Create(request);
      
      return Ok(new RegisterUser.Response(
            user.Login,
            user.FirstName,
            user.LastName,
            user.Phone));
   }


   [AllowAnonymous]
   [HttpPost]
   public async Task<IActionResult> Login([FromBody] LoginUser request)
   {
      if (!ModelState.IsValid)
      {
         return BadRequest();
      }

      var user = await ServiceFactory.UserService.ValidateUser(request);
 
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