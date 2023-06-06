using Microsoft.Extensions.Logging;
using VoteApp.Database.Candidate;
using VoteApp.Database.CandidateDocument;
using VoteApp.Database.Document;
using VoteApp.Database.Document.Statistic;
using VoteApp.Database.User;

namespace VoteApp.Database
{

    public class DatabaseContainer : IDatabaseContainer
    {
        public IUserRepository User { get; }
        public IDocumentRepository Document { get; }
        public ICandidateDocumentRepository CandidateDocument { get; set; }
        public IDocumentStatisticRepository DocumentStatistic { get; }
        public ICandidateRepository Candidate { get; }
        
        public DatabaseContainer(PostgresContext db, ILoggerFactory loggerFactory)
        {
            User = new UserRepository(db, loggerFactory);
            Document = new DocumentRepository(db, loggerFactory);
            CandidateDocument = new CandidateDocumentRepository(db, loggerFactory);
            DocumentStatistic = new DocumentStatisticRepository(db, loggerFactory);
            Candidate = new CandidateRepository(db, loggerFactory);
        }
    }
}