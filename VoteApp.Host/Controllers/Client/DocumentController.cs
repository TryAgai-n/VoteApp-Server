using Microsoft.AspNetCore.Mvc;
using VoteApp.Database.Document;
using VoteApp.Host.Service;
using VoteApp.Host.Utils;

namespace VoteApp.Host.Controllers.Client;

public class DocumentController: AbstractClientController
{
    public DocumentController(
        IServiceFactory serviceFactory,
        IUtilsFactory utilsFactory
        ) : base(
        serviceFactory,
        utilsFactory) { }


    [HttpGet]
    public async Task<IActionResult> GetDocumentList(int skip, int take)
    {
        var documents = await ServiceFactory.DocumentService.ListDocumentsByStatus(DocumentStatus.Default, skip, take);
        
        if (documents.Count is 0)
        {
            return NoContent();
        }
        
        return Ok(documents);
    }


}