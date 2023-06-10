using Microsoft.AspNetCore.Mvc;
using VoteApp.Database.Document;
using VoteApp.Host.Service.Document;

namespace VoteApp.Host.Controllers.Client;

public class DocumentController: AbstractClientController
{
    
    private readonly IDocumentService _documentService;

    public DocumentController(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetDocumentList(int skip, int take)
    {
        var documents = await _documentService.ListDocumentsByStatus(DocumentStatus.Default, skip, take);
        
        if (documents.Count is 0)
        {
            return NoContent();
        }
        
        return Ok(documents);
    }
}