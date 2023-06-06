using Microsoft.AspNetCore.Mvc;
using VoteApp.Database;
using VoteApp.Database.Document;
using VoteApp.Host.Service.Candidate;
using VoteApp.Host.Service.Document;
using VoteApp.Host.Service.User;
using VoteApp.Models.API.Candidate;

namespace VoteApp.Host.Controllers.Client;

public class CandidateController : AbstractClientController
{
    private readonly IUserService _userService;
    private readonly ICandidateService _candidateService;
    private readonly IDocumentService _documentService;


    public CandidateController(
        IDatabaseContainer databaseContainer,
        IUserService userService,
        ICandidateService candidateService,
        IDocumentService documentService
    ) : base(databaseContainer)
    {
        _userService = userService;
        _candidateService = candidateService;
        _documentService = documentService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RequestCreateCandidate request)
    {
        
        var userId = await _userService.GetUserIdFromValidCookies(HttpContext);
        
        var candidate = await _candidateService.Create(request.Description, userId);

        return Ok(candidate);

    }


    [HttpPost]
    public async Task<IActionResult> UploadDocumentToCandidate(IFormFile photo, int candidateId)
    {
        var userId = await _userService.GetUserIdFromValidCookies(HttpContext);
        
        var candidate = await _candidateService.GetOneById(candidateId, userId);

        await _documentService.ValidatePhoto(photo);

        var uploadPhoto = await _documentService.UploadDocument(userId, photo, DocumentStatus.Default);

        var result = await DatabaseContainer.CandidateDocument.Create(candidate.Id, uploadPhoto.Id);
        
        return Ok();
    }

}