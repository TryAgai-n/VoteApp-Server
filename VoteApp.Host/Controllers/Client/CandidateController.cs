using Microsoft.AspNetCore.Mvc;
using VoteApp.Database;
using VoteApp.Database.Candidate;
using VoteApp.Database.CandidateDocument;
using VoteApp.Host.Service;

namespace VoteApp.Host.Controllers.Client;

public class CandidateController : AbstractClientController
{
    private readonly IUserService _userService;


    public CandidateController(
        IDatabaseContainer databaseContainer,
        IUserService userService) : base(databaseContainer)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Create()
    {

        var userId = await _userService.GetUserIdFromValidCookies(HttpContext);

        var candidate = new CandidateModel
        {
            Id = 1,
            UserId = 1,
            PreviewDocumentId = 2,
            Description = "Hello World",
            CandidateDocuments = new List<CandidateDocumentModel>(),
        };

        return Ok();

    }


    [HttpPost]
    public async Task<IActionResult> AddDocumentToCandidate()
    {

        var userId = await _userService.GetUserIdFromValidCookies(HttpContext);

        return Ok();

    }
    

}