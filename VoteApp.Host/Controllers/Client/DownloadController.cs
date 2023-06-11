using Microsoft.AspNetCore.Mvc;
using VoteApp.Database.Document;
using VoteApp.Host.Service;
using VoteApp.Host.Utils;


namespace VoteApp.Host.Controllers.Client;

public class DownloadController : AbstractClientController
{

    public DownloadController(
        IServiceFactory serviceFactory,
        IUtilsFactory utilsFactory
        ) : base(
        serviceFactory,
        utilsFactory) { }

    
    [HttpGet]
    public async Task<IActionResult> DownloadFiles(int skip, int take, DocumentQuality quality)
    {
        var documents = await ServiceFactory.DocumentService.ListDocumentsByStatus(DocumentStatus.Default, skip, take);

        if (documents.Count is 0)
        {
            return NoContent();
        }
        
        return await UtilsFactory.DocumentUtils.CreateZipArchive(documents, quality);
    }

    [HttpGet]
    public async Task<IActionResult> DownloadFile(int documentId, DocumentQuality quality)
    {
        var document = await ServiceFactory.DocumentService.GetDocumentById(documentId);
        return await UtilsFactory.DocumentUtils.GetDocumentFile(document, quality);
    }


}