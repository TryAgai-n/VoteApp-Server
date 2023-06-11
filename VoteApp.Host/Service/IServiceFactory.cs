using VoteApp.Host.Service.Candidate;
using VoteApp.Host.Service.Document;
using VoteApp.Host.Service.User;

namespace VoteApp.Host.Service;

public interface IServiceFactory
{
    public ICandidateService CandidateService { get; set; }
    public IDocumentService DocumentService { get; set; }
    public IUserService UserService { get; set; }
}