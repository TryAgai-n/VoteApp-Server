using Microsoft.AspNetCore.Mvc;
using VoteApp.Database.Document;

namespace VoteApp.Host.Service.Document;

public interface IDocumentService
{
    Task<IActionResult> CreateZipArchive(List<DocumentModel> documents, DocumentQuality documentQuality);

    Task<IFormFile> ValidatePhoto(IFormFile photo);

    Task<IActionResult> GetDocumentFile(DocumentModel document, DocumentQuality documentQuality);

    Task<DocumentModel> UploadDocument(int userId, IFormFile photo, DocumentStatus documentStatus);
}