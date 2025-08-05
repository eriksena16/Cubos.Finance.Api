using Cubos.Finance.Application;
using Cubos.Finance.Domain;
using Cubos.Finance.Shared;
using Moq;
using Xunit;

namespace Cubos.Finance.Tests
{
    public class BankAccountServiceTests
    {
        private readonly Mock<IBankAccountRepository> _accountRepositoryMock = new();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        private readonly Mock<INotifier> _notifierMock = new();

        private readonly BankAccountService _service;

        public BankAccountServiceTests()
        {
            _service = new BankAccountService(
                _notifierMock.Object,
                _unitOfWorkMock.Object,
                _accountRepositoryMock.Object
            );
        }

        [Fact]
        public async Task CreateAsync_Should_CreateAccount_When_NotExists()
        {
            // Arrange
            var peopleId = Guid.NewGuid();
            var request = new BankAccountRequest { Account = "6013031-2", Branch = "001" };
            var createdAccount = new BankAccount { Id = Guid.NewGuid(), Branch = "001", Account = "6013031-2" };
            _accountRepositoryMock.Setup(r => r.HasBankAccountAsync(request.Account))
                .ReturnsAsync(false);

            _accountRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<BankAccount>()))
                .ReturnsAsync(createdAccount);

            _unitOfWorkMock.Setup(u => u.CommitAsync())
                .ReturnsAsync(1);

            // Act
            var response = await _service.CreateAsync(peopleId, request);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(request.Account, response.Account);
            Assert.Equal(request.Branch, response.Branch);

            _accountRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<BankAccount>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
            _notifierMock.Verify(n => n.Handle(It.IsAny<Notification>()), Times.Never);
        }

        [Fact]
        public async Task CreateAsync_Should_NotifyAndReturnNull_When_AccountAlreadyExists()
        {
            // Arrange
            var peopleId = Guid.NewGuid();
            var request = new BankAccountRequest { Account = "12345-6" };

            _accountRepositoryMock.Setup(r => r.HasBankAccountAsync(request.Account))
                .ReturnsAsync(true);

            // Act
            var response = await _service.CreateAsync(peopleId, request);

            // Assert
            Assert.Null(response);

            _notifierMock.Verify(n => n.Handle(It.Is<Notification>(notif =>
                notif.Message == CubosErrorMessages.ACCOUNT_ALREADY_EXISTS)), Times.Once);

            _accountRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<BankAccount>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Never);
        }
    }

}

