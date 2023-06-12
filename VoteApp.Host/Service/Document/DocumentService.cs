using VoteApp.Database;
using VoteApp.Database.Document;

namespace VoteApp.Host.Service.Document;

public class DocumentService : IDocumentService
{
    private readonly IDatabaseContainer _databaseContainer;

    public DocumentService(IDatabaseContainer databaseContainer)
    {
        _databaseContainer = databaseContainer;
    }
    
    public async Task<List<DocumentModel>> ListDocumentsByStatus(DocumentStatus status, int skip, int take)
    {
        return await _databaseContainer.Document.ListDocumentsByStatus(status, skip, take);
    }
    
    public async Task<DocumentModel> GetDocumentById(int documentId)
    {
        return await _databaseContainer.Document.GetDocumentById(documentId);
    }
}