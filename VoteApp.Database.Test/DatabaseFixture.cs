using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using VoteApp.Database;
using VoteApp.Migrations;

namespace VoteApp.Database.Test;

public class DatabaseFixture : IDisposable
{
    private readonly PostgresContext _context;
    public readonly DatabaseContainer DatabaseContainer;

    public DatabaseFixture()
    {
        var guid = Guid.NewGuid().ToString("N");

        var options = new DbContextOptionsBuilder<PostgresContext>()
            .UseNpgsql(
                "User ID=postgres;Password=123;Host=localhost;Port=5432;Database=VoteApp_test_" + guid + ";Pooling=true;",
                b => b.MigrationsAssembly(typeof(Startup).Assembly.GetName().Name))
            .Options;

        _context = new PostgresContext(options, new NullLoggerFactory());
        _context.Database.Migrate();

        DatabaseContainer = _context.Db;
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}