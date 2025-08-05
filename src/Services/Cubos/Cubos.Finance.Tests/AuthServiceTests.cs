using Cubos.Finance.Application;
using Cubos.Finance.Domain;
using Cubos.Finance.Shared;
using Moq;
using Xunit;
namespace Cubos.Finance.Tests
{
    public class AuthServiceTests
    {
        private readonly Mock<IPeopleRepository> _repositoryMock;
        private readonly Mock<IJwtService> _jwtServiceMock;
        private readonly Mock<INotifier> _notifierMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _repositoryMock = new Mock<IPeopleRepository>();
            _jwtServiceMock = new Mock<IJwtService>();
            _notifierMock = new Mock<INotifier>();
            _authService = new AuthService(_notifierMock.Object, _repositoryMock.Object, _jwtServiceMock.Object);
        }

        [Fact]
        public async Task AuthenticateAsync_ValidCredentials_ReturnsToken()
        {
            // Arrange
            var loginRequest = new LoginRequest
            {
                Document = "123.456.789-00",
                Password = "senha123"
            };

            var person = new People
            {
                Id = Guid.NewGuid(),
                Document = "12345678900", // document limpado
                Password = PasswordHasher.Hash("senha123")
            };

            var expectedToken = new BearerToken { Token = "jwt-fake-token" };

            _repositoryMock
                .Setup(r => r.GetByDocumentAsync("12345678900"))
                .ReturnsAsync(person);

            _jwtServiceMock
                .Setup(j => j.GenerateAccessToken(person.Id.ToString()))
                .Returns(expectedToken);

            // Act
            var result = await _authService.AuthenticateAsync(loginRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("jwt-fake-token", result.Token);
        }


    }
}
