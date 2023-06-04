using Microsoft.AspNetCore.Mvc;
using VoteApp.Database;
using VoteApp.Database.Document;
using VoteApp.Host.Service;


namespace VoteApp.Host.Controllers.Client;

public class UploadController : AbstractClientController
{
    private readonly IDocumentService _documentService;
    private readonly IUserService _userService;


    public UploadController(
        IDatabaseContainer databaseContainer,
        IDocumentService documentService,
        IUserService userService) : base(databaseContainer)
    {
        _documentService = documentService;
        _userService = userService;
    }


    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile photo)
    {
        var userId = await _userService.GetUserIdFromValidCookies(HttpContext);
        
        var user = await DatabaseContainer.UserWeb.GetOneById(userId);

        await _documentService.ValidatePhoto(photo);

        await _documentService.UploadDocument(user.Id, photo, DocumentStatus.Default);

        return Ok();

    }
}