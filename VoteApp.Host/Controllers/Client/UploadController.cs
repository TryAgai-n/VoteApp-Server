using Microsoft.AspNetCore.Mvc;
using VoteApp.Database.Document;
using VoteApp.Host.Service;
using VoteApp.Host.Utils;

namespace VoteApp.Host.Controllers.Client;

public class UploadController : AbstractClientController
{

    public UploadController(
        IServiceFactory serviceFactory,
        IUtilsFactory utilsFactory)
        : base(
            serviceFactory,
            utilsFactory) { }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile photo)
    {
        var userId = await UtilsFactory.UserUtils.GetUserIdFromCookies(HttpContext);
        
        var user = await ServiceFactory.UserService.GetOneById(userId);

        await UtilsFactory.DocumentUtils.ValidatePhoto(photo);

        await UtilsFactory.DocumentUtils.UploadDocument(user.Id, photo, DocumentStatus.Default);

        return Ok();
    }


}