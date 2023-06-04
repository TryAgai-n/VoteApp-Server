
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VoteApp.Database.Candidate;
using VoteApp.Database.CandidateDocument;
using VoteApp.Database.Document;
using VoteApp.Database.User;

namespace VoteApp.Database
{
    public class PostgresContext : DbContext
    {
        public readonly DatabaseContainer Db;
        
        
        public DbSet<UserModel> User { get; set; }
        
        public DbSet<DocumentModel> Document { get; set; }
        
        public DbSet<CandidateModel> Candidate { get; set; }
        
        public DbSet<CandidateDocumentModel> CandidateDocument { get; set; }


        public PostgresContext(DbContextOptions<PostgresContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            Db = new DatabaseContainer(this, loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureUser(modelBuilder);
            ConfigureDocument(modelBuilder);
            ConfigureCandidate(modelBuilder);
            ConfigureCandidateDocument(modelBuilder);
        }
        
        private void ConfigureUser(ModelBuilder builder)
        {
            builder.Entity<UserModel>()
                .HasKey(d => d.Id);

            builder.Entity<UserModel>()
                .Property(d => d.Id)
                .UseIdentityColumn();
            
            builder.Entity<DocumentModel>()
                .HasOne(x => x.UserModel)
                .WithMany(x => x.Documents)
                .HasForeignKey(d => d.UserId);
            
            builder.Entity<CandidateModel>()
                .HasOne(x => x.User)
                .WithMany(x => x.Candidates)
                .HasForeignKey(d => d.UserId);
        }

        private void ConfigureDocument(ModelBuilder builder)
        {
            builder.Entity<DocumentModel>()
                .HasKey(d => d.Id);

            builder.Entity<DocumentModel>()
                .Property(d => d.Id)
                .UseIdentityColumn();

        }
        
        private void ConfigureCandidate(ModelBuilder builder)
        {
            
            builder.Entity<CandidateModel>()
                .HasKey(d => d.Id);

            builder.Entity<CandidateModel>()
                .Property(d => d.Id)
                .UseIdentityColumn();
            
        }
        
        private void ConfigureCandidateDocument(ModelBuilder builder)
        {
            builder.Entity<CandidateDocumentModel>()
                .HasKey(cd => new { cd.CandidateId, cd.DocumentId });

            builder.Entity<CandidateDocumentModel>()
                .HasOne(cd => cd.Candidate)
                .WithMany(c => c.CandidateDocuments)
                .HasForeignKey(cd => cd.CandidateId);

            builder.Entity<CandidateDocumentModel>()
                .HasOne(cd => cd.Document)
                .WithMany(d => d.CandidateDocuments)
                .HasForeignKey(cd => cd.DocumentId);
        }
    }
}
