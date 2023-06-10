using Microsoft.AspNetCore.Mvc;
using VoteApp.Database.Document;
using VoteApp.Host.Service.Document;
using VoteApp.Host.Utils.DocumentUtils;

namespace VoteApp.Host.Controllers.Client;

public class DownloadController : AbstractClientController
{
    private readonly IDocumentService _documentService;
    private readonly IDocumentUtils _documentUtils;
    
    public DownloadController(
        IDocumentService documentService,
        IDocumentUtils documentUtils
    )
    {
        _documentService = documentService;
        _documentUtils = documentUtils;
    }

    
    [HttpGet]
    public async Task<IActionResult> DownloadFiles(int skip, int take, DocumentQuality quality)
    {
        var documents = await _documentService.ListDocumentsByStatus(DocumentStatus.Default, skip, take);

        if (documents.Count is 0)
        {
            return NoContent();
        }
        
        return await _documentUtils.CreateZipArchive(documents, quality);
    }

    [HttpGet]
    public async Task<IActionResult> DownloadFile(int documentId, DocumentQuality quality)
    {
        var document = await _documentService.GetDocumentById(documentId);
        return await _documentUtils.GetDocumentFile(document, quality);
    }
}