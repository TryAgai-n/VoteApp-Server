using Microsoft.AspNetCore.Mvc;
using VoteApp.Database;
using VoteApp.Database.Candidate;
using VoteApp.Host.Service;
using VoteApp.API.Models.Candidate;
using VoteApp.API.Models.User;

namespace VoteApp.Host.Controllers.Admin;

public class AdminController : AbstractAdminController
{
    private readonly IServiceFactory _serviceFactory;

    
    public AdminController(IDatabaseContainer databaseContainer, IServiceFactory serviceFactory) : base(databaseContainer)
    {
        _serviceFactory = serviceFactory;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(FullUserInfo.Response), 200)]
    public async Task<IActionResult> UserById(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
      
        var user = await DatabaseContainer.User.GetOneById(id);

        return Ok(
            new FullUserInfo.Response(
                user.Login,
                user.FirstName,
                user.LastName,
                user.Phone,
                user.UserRole,
                user.Documents.Select(x=> x.Id).ToList(),
                user.Candidates.Select(x=> x.Id).ToList()
            )
        );
    }
   

    [HttpPost]
    [ProducesResponseType(typeof(RegisterUser.Response), 200)]
    
    public async Task<IActionResult> CreateUser(RegisterUser requestUser)
    {
        var user = await DatabaseContainer.User.CreateUser(
            requestUser.Login,
            requestUser.FirstName,
            requestUser.LastName, 
            requestUser.Phone, 
            requestUser.Password);
        
        return Ok(user);
    }
    
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CandidateList.Response>), 200)]
    public async Task<IActionResult> GetCandidateListByStatus(CandidateStatus status, int skip, int take)
    {
        var candidates = await _serviceFactory.CandidateService.ListCandidateByStatus(status, skip, take);

        return Ok(
            candidates.Select(
                c => new CandidateList.Response(
                    c.Id,
                    c.Description,
                    c.PreviewDocumentId)));
    }
}