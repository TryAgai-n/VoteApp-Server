using Bogus;
using VoteApp.Database.User;

namespace VoteApp.Database.Test
{
    public class UserTest : DbTestCase
    {
        public UserTest(DatabaseFixture fixture) : base(fixture) { }

        [Fact]
        public async Task CreateUser_ReturnsCreatedUser()
        {
            // Arrange
            const string login = "test";
            const string firstName = "John";
            const string lastName = "Doe";
            const string phone = "123456789";
            const string password = "password";

            // Act
            var user =  await DatabaseContainer.User.CreateUser(login, firstName, lastName, phone, password);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(login, user.Login);
            Assert.Equal(firstName, user.FirstName);
            Assert.Equal(lastName, user.LastName);
            Assert.Equal(phone, user.Phone);
            Assert.Equal(password, user.Password);
            Assert.Equal(UserRole.User, user.UserRole);
            Assert.NotEqual(0, user.Id);
        }

        [Fact]
        public async Task GetOneById_ExistingId_ReturnsUser()
        {
            // Arrange
            var user = await CreateUser();

            // Act
            var result = await DatabaseContainer.User.GetOneById(user.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Login, result.Login);
        }

        [Fact]
        public async Task GetOneById_NonExistingId_ThrowsArgumentException()
        {
            // Arrange
            var nonExistingId = 9999;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await DatabaseContainer.User.GetOneById(nonExistingId);
            });
        }

        [Fact]
        public async Task FindOneByLogin_ExistingLogin_ReturnsUser()
        {
            // Arrange
            var user = await CreateUser();

            // Act
            var result = await DatabaseContainer.User.FindOneByLogin(user.Login);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Login, result.Login);
        }

        [Fact]
        public async Task FindOneByLogin_NonExistingLogin_ThrowsArgumentException()
        {
            // Arrange
            const string nonExistingLogin = "nonExisting";

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await DatabaseContainer.User.FindOneByLogin(nonExistingLogin);
            });
        }

        [Fact]
        public async Task GetOneByPhone_ExistingPhone_ReturnsUser()
        {
            // Arrange
            var user = await CreateUser();

            // Act
            var result = await DatabaseContainer.User.GetOneByPhone(user.Phone);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Phone, result.Phone);
        }

        [Fact]
        public async Task GetOneByPhone_NonExistingPhone_ThrowsArgumentException()
        {
            // Arrange
            const string nonExistingPhone = "9999999999";

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await DatabaseContainer.User.GetOneByPhone(nonExistingPhone);
            });
        }

        private async Task<UserModel> CreateUser()
        {
            var faker = new Faker();

            var login = faker.Internet.UserName();
            var firstName = faker.Name.FirstName();
            var lastName = faker.Name.LastName();
            var phone = faker.Phone.PhoneNumber();
            var password = faker.Internet.Password();

            return await DatabaseContainer.User.CreateUser(login, firstName, lastName, phone, password);
        }
    }
}
