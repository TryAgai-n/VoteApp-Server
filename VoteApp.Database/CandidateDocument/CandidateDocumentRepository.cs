using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace VoteApp.Database.CandidateDocument;

public class CandidateDocumentRepository: AbstractRepository<CandidateDocumentModel>, ICandidateDocumentRepository
{
    
    public CandidateDocumentRepository(PostgresContext context, ILoggerFactory loggerFactory) : base(context, loggerFactory) { }

    public async Task<CandidateDocumentModel> Create(int candidateId, int documentId)
    {
        var model = CandidateDocumentModel.Create(candidateId, documentId);
        
        var result = await CreateModelAsync(model);
        
        if(model is null)
        {
            throw new ArgumentException("Candidate-Document is not created. Instantiate error");
        }
            
        return result;
        
    }
}