using Xunit;

namespace VoteApp.Database.Test;

public class DbTestCase : IClassFixture<DatabaseFixture>
{
    protected readonly DatabaseContainer DatabaseContainer;
    protected DbTestCase(DatabaseFixture fixture)
    {
        DatabaseContainer = fixture.DatabaseContainer;
    }
}