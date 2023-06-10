using Microsoft.AspNetCore.Mvc;
using VoteApp.Database.Document;
using VoteApp.Host.Service.Document;
using VoteApp.Host.Service.User;
using VoteApp.Host.Utils.DocumentUtils;
using VoteApp.Host.Utils.UserUtils;


namespace VoteApp.Host.Controllers.Client;

public class UploadController : AbstractClientController
{
    private readonly IDocumentService _documentService;
    private readonly IUserService _userService;
    private readonly IDocumentUtils _documentUtils;
    private readonly IUserUtils _userUtils;


    public UploadController(
        IDocumentService documentService,
        IUserService userService,
        IDocumentUtils documentUtils,
        IUserUtils userUtils
    ) 
    {
        _documentService = documentService;
        _userService = userService;
        _documentUtils = documentUtils;
        _userUtils = userUtils;
    }


    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile photo)
    {
        var userId = await _userUtils.GetUserIdFromCookies(HttpContext);
        
        var user = await _userService.GetOneById(userId);

        await _documentUtils.ValidatePhoto(photo);

        await _documentUtils.UploadDocument(user.Id, photo, DocumentStatus.Default);

        return Ok();
    }
}