using VoteApp.Host.Service.Candidate;
using VoteApp.Host.Service.Document;
using VoteApp.Host.Service.User;

namespace VoteApp.Host.Service;

internal class ServiceFactory : IServiceFactory
{
    public ICandidateService CandidateService { get; set; }
    public IDocumentService DocumentService { get; set; }
    public IUserService UserService { get; set; }
    
    
    public ServiceFactory(
        ICandidateService candidateService,
        IDocumentService documentService,
        IUserService userService)
    {
        CandidateService = candidateService;
        DocumentService = documentService;
        UserService = userService;
    }
}