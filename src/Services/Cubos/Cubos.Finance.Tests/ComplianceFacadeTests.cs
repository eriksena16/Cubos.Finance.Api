using Xunit;
using Moq;
using Cubos.Finance.External;
namespace Cubos.Finance.Tests
{
    public class ComplianceFacadeTests
    {
        private readonly Mock<IComplianceClient> _clientMock;
        private readonly ComplianceFacade _facade;

        public ComplianceFacadeTests()
        {
            _clientMock = new Mock<IComplianceClient>();
            _facade = new ComplianceFacade(_clientMock.Object);
        }

        [Fact(DisplayName = "Should validate CPF correctly")]
        [Trait("Finance", "Compliance - Document Validation")]
        public async Task IsDocumentValidAsync_ShouldCallValidateCpf_WhenCpfIsValid()
        {
            // Arrange
            var cpf = "12345678901";
            _clientMock.Setup(c => c.ValidateCpfAsync(It.IsAny<DocumentRequest>()))
                .ReturnsAsync(new ComplianceResponse { Status = 1 });

            // Act
            var result = await _facade.IsDocumentValidAsync(cpf);

            // Assert
            Assert.True(result);
            _clientMock.Verify(c => c.ValidateCpfAsync(It.Is<DocumentRequest>(r => r.Document == cpf)), Times.Once);
            _clientMock.Verify(c => c.ValidateCnpjAsync(It.IsAny<DocumentRequest>()), Times.Never);
        }

        [Fact(DisplayName = "Should validate CNPJ correctly")]
        [Trait("Finance", "Compliance - Document Validation")]
        public async Task IsDocumentValidAsync_ShouldCallValidateCnpj_WhenCnpjIsValid()
        {
            // Arrange
            var cnpj = "12345678000199";
            _clientMock.Setup(c => c.ValidateCnpjAsync(It.IsAny<DocumentRequest>()))
                .ReturnsAsync(new ComplianceResponse { Status = 1 });

            // Act
            var result = await _facade.IsDocumentValidAsync(cnpj);

            // Assert
            Assert.True(result);
            _clientMock.Verify(c => c.ValidateCnpjAsync(It.Is<DocumentRequest>(r => r.Document == cnpj)), Times.Once);
            _clientMock.Verify(c => c.ValidateCpfAsync(It.IsAny<DocumentRequest>()), Times.Never);
        }

        [Fact(DisplayName = "Should return false when CPF status is not 1")]
        [Trait("Finance", "Compliance - Document Validation")]
        public async Task IsDocumentValidAsync_ShouldReturnFalse_WhenCpfStatusIsDifferentFrom1()
        {
            // Arrange
            var cpf = "12345678901";
            _clientMock.Setup(c => c.ValidateCpfAsync(It.IsAny<DocumentRequest>()))
                .ReturnsAsync(new ComplianceResponse { Status = 0 });

            // Act
            var result = await _facade.IsDocumentValidAsync(cpf);

            // Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Should throw exception when document is invalid")]
        [Trait("Finance", "Compliance - Document Validation")]
        public async Task IsDocumentValidAsync_ShouldThrowArgumentException_WhenDocumentIsInvalid()
        {
            // Arrange
            var invalidDoc = "123";

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _facade.IsDocumentValidAsync(invalidDoc));
            Assert.Equal("Documento inválido: deve conter 11 (CPF) ou 14 (CNPJ) dígitos.", ex.Message);
        }
    }

}
