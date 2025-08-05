using Cubos.Finance.Application;
using Cubos.Finance.Domain;
using Cubos.Finance.Shared;
using Moq;
using System.Globalization;
using Xunit;

namespace Cubos.Finance.Tests
{
    public class TransactionServiceTests
    {
        private readonly Mock<ITransactionRepository> _transactionRepositoryMock = new();
        private readonly Mock<IBankAccountRepository> _bankAccountRepositoryMock = new();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        private readonly Mock<INotifier> _notifierMock = new();

        private readonly TransactionService _transactionService;

        public TransactionServiceTests()
        {
            _transactionService = new TransactionService(
                _notifierMock.Object,
                _unitOfWorkMock.Object,
                _bankAccountRepositoryMock.Object,
                _transactionRepositoryMock.Object
            );
        }

        [Fact]
        public async Task RegisterTransactionAsync_Should_ReturnNull_WhenValueIsZero()
        {
            // Arrange
            var request = new TransactionRequest { Value = 0 };

            // Act
            var result = await _transactionService.RegisterTransactionAsync(Guid.NewGuid(), request);

            // Assert
            Assert.Null(result);
            _notifierMock.Verify(n => n.Handle(It.Is<Notification>(notif => notif.Message == CubosErrorMessages.INVALID_TRANSACTION_VALUE)), Times.Once);

        }

        [Fact]
        public async Task RegisterTransactionAsync_Should_ReturnNull_WhenAccountDoesNotExist()
        {
            // Arrange
            var request = new TransactionRequest { Value = 100 };
            _bankAccountRepositoryMock.Setup(r => r.GetAccountByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((BankAccount)null);

            // Act
            var result = await _transactionService.RegisterTransactionAsync(Guid.NewGuid(), request);

            // Assert
            Assert.Null(result);
            _notifierMock.Verify(n => n.Handle(It.Is<Notification>(notif => notif.Message == CubosErrorMessages.ACCOUNT_NOT_FOUND)), Times.Once);
        }

        [Fact]
        public async Task RegisterTransactionAsync_Should_ReturnNull_WhenInsufficientBalance()
        {
            // Arrange
            var account = new BankAccount { Id = Guid.NewGuid(), Branch = "001", Account = "6013031-2" };
            _bankAccountRepositoryMock.Setup(r => r.GetAccountByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(account);

            var request = new TransactionRequest { Value = -500 };

            // Act
            var result = await _transactionService.RegisterTransactionAsync(account.Id, request);

            // Assert
            Assert.Null(result);
            var expectedMessage = $"Saldo de {account.Balance.ToString("C", new CultureInfo("pt-BR"))} insuficiente para realizar a transação {request.Value.ToString("C", new CultureInfo("pt-BR"))}.";
            _notifierMock.Verify(n => n.Handle(It.Is<Notification>(notif => notif.Message == expectedMessage)), Times.Once);
        }

        [Fact]
        public async Task RegisterTransactionAsync_Should_CreateTransaction_WhenValid()
        {
            // Arrange
            var account = new BankAccount { Id = Guid.NewGuid(), Branch = "001", Account = "6013031-2" };
            _bankAccountRepositoryMock.Setup(r => r.GetAccountByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(account);

            var request = new TransactionRequest { Value = 100, Description = "Compra" };
            // Act
            var result = await _transactionService.RegisterTransactionAsync(account.Id, request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(request.Description, result.Description);
            _transactionRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Transaction>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        }
    }

}

