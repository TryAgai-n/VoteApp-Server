using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoteApp.Host.ExceptionFilter;
using VoteApp.Host.Service;
using VoteApp.Host.Utils;

namespace VoteApp.Host.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]/[action]")]
[CustomException]
public abstract class AbstractClientController : ControllerBase
{
    protected readonly IServiceFactory ServiceFactory;
    protected readonly IUtilsFactory UtilsFactory;
    
    protected AbstractClientController(
        IServiceFactory serviceFactory,
        IUtilsFactory utilsFactory)
    {
        ServiceFactory = serviceFactory;
        UtilsFactory = utilsFactory;
    }
}