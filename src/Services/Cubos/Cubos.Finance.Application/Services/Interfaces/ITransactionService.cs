using Cubos.Finance.Domain;
using Cubos.Finance.Shared;

namespace Cubos.Finance.Application
{
    public interface ITransactionService
    {
        /// <summary>
        /// Registra uma transação na conta bancária, atualizando o saldo.
        /// Valida existência da conta e saldo suficiente.
        /// </summary>

        Task<TransactionResponse> RegisterTransactionAsync(Guid bankAccountId, TransactionRequest request);

        /// <summary>
        /// Registra uma transação na conta bancária interna, atualizando o saldo.
        /// Valida existência da conta e saldo suficiente.
        /// </summary>
        Task<TransactionInternalResponse> RegisterTransactionInternalAsync(Guid bankAccountId, TransactionRequest request);
        Task<QueryBaseResponse<TransactionResponse>> GetTransactionsAsync(Guid bankAccountId, TransactionPaginationRequest filterRequest);
        Task<TransactionResponse> RevertTransactionAsync(Guid accountId, Guid transactionId);
    }
}
