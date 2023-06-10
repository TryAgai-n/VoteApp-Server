using Microsoft.AspNetCore.Mvc;
using VoteApp.Database.Candidate;
using VoteApp.Database.Document;
using VoteApp.Host.Service.Candidate;
using VoteApp.Host.Service.Document;
using VoteApp.Host.Service.User;
using VoteApp.Host.Utils.DocumentUtils;
using VoteApp.Host.Utils.UserUtils;
using VoteApp.Models.API.Candidate;

namespace VoteApp.Host.Controllers.Client;

public class CandidateController : AbstractClientController
{
    private readonly IUserService _userService;
    private readonly ICandidateService _candidateService;
    private readonly IDocumentService _documentService;
    private readonly IDocumentUtils _documentUtils;
    private readonly IUserUtils _userUtils;


    public CandidateController(
        IUserService userService,
        ICandidateService candidateService,
        IDocumentService documentService,
        IDocumentUtils documentUtils,
        IUserUtils userUtils
    ) 
    {
        _userService = userService;
        _candidateService = candidateService;
        _documentService = documentService;
        _documentUtils = documentUtils;
        _userUtils = userUtils;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] string description)
    {
        var userId = await _userUtils.GetUserIdFromCookies(HttpContext);
        
        var candidate = await _candidateService.Create(description, userId);

        return Ok(candidate);
    }

    [HttpPost]
    public async Task<IActionResult> UploadDocumentToCandidate(IFormFile photo, int candidateId)
    {
        var userId = await _userUtils.GetUserIdFromCookies(HttpContext);
        
        var candidate = await _candidateService.GetOneById(candidateId);

        await _candidateService.IsUsersCandidate(userId, candidate);

        await _documentUtils.ValidatePhoto(photo);

        var uploadPhoto = await _documentUtils.UploadDocument(userId, photo, DocumentStatus.Default);
        
        await _candidateService.CreateCandidateDocument(candidate.Id, uploadPhoto.Id);
        
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> GetCandidateById([FromBody] int candidateId)
    {
        var candidate = await _candidateService.GetOneById(candidateId);
        
        return Ok( 
            new Candidate.Response(
            candidate.Id,
            candidate.Description,
            candidate.PreviewDocumentId,
            candidate.UserId,
            candidate.CandidateDocuments.Select(x => x.DocumentId).ToList()));
        
    }


    [HttpGet]
    public async Task<IActionResult> GetCandidateList(int skip, int take)
    {
        var candidates = await _candidateService.ListCandidateByStatus(CandidateStatus.Approve, skip, take);

        return Ok(candidates.Select(
            c => new CandidateList.Response(
                c.Id,
                c.Description,
                c.PreviewDocumentId)));
    }

}