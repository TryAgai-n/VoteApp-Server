using VoteApp.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoteApp.Host.ExceptionFilter;

namespace VoteApp.Host.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]/[action]")]
[CustomException]
public abstract class AbstractAdminController: ControllerBase
{
    protected readonly IDatabaseContainer DatabaseContainer;
    
    protected AbstractAdminController(IDatabaseContainer databaseContainer)
    {
        DatabaseContainer = databaseContainer;
    }
}