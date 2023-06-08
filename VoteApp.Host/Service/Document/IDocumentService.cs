using VoteApp.Database.Document;

namespace VoteApp.Host.Service.Document;

public interface IDocumentService
{
   Task<List<DocumentModel>> ListDocumentsByStatus(DocumentStatus status, int skip, int take);
   
   Task<DocumentModel> GetDocumentById(int documentId);
}