using VoteApp.Database.Candidate;
using VoteApp.Database.CandidateDocument;
using VoteApp.Database.Document;
using VoteApp.Database.Document.Statistic;
using VoteApp.Database.User;

namespace VoteApp.Database
{
    public interface IDatabaseContainer
    {
        IUserRepository User { get; }
        ICandidateRepository Candidate { get; }
        ICandidateDocumentRepository CandidateDocument { get; }
        IDocumentRepository Document { get; }
        IDocumentStatisticRepository DocumentStatistic { get; }
    }
}
