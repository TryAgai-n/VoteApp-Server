using Microsoft.AspNetCore.Mvc;
using VoteApp.Database.Candidate;
using VoteApp.Database.Document;
using VoteApp.Host.Service;
using VoteApp.Host.Utils;
using VoteApp.Models.API.Candidate;

namespace VoteApp.Host.Controllers.Client;

public class CandidateController : AbstractClientController
{
    public CandidateController(IServiceFactory serviceFactory, IUtilsFactory utilsFactory) 
        : base(serviceFactory, utilsFactory) { }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] string description)
    {
        var userId = await UtilsFactory.UserUtils.GetUserIdFromCookies(HttpContext);
        
        var candidate = await ServiceFactory.CandidateService.Create(description, userId);

        return Ok(candidate);
    }

    [HttpPost]
    public async Task<IActionResult> UploadDocumentToCandidate(IFormFile photo, int candidateId)
    {
        var userId = await UtilsFactory.UserUtils.GetUserIdFromCookies(HttpContext);
        
        var candidate = await ServiceFactory.CandidateService.GetOneById(candidateId);

        await ServiceFactory.CandidateService.IsUsersCandidate(userId, candidate);

        await UtilsFactory.DocumentUtils.ValidatePhoto(photo);

        var uploadPhoto = await UtilsFactory.DocumentUtils.UploadDocument(userId, photo, DocumentStatus.Default);
        
        await ServiceFactory.CandidateService.CreateCandidateDocument(candidate.Id, uploadPhoto.Id);
        
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> GetCandidateById([FromBody] int candidateId)
    {
        var candidate = await ServiceFactory.CandidateService.GetOneById(candidateId);
        
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
        var candidates = await ServiceFactory.CandidateService.ListCandidateByStatus(CandidateStatus.Approve, skip, take);

        return Ok(candidates.Select(
            c => new CandidateList.Response(
                c.Id,
                c.Description,
                c.PreviewDocumentId)));
    }
}