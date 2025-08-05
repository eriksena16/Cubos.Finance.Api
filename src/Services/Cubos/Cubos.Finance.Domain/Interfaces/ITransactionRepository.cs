using Cubos.Finance.Shared;

namespace Cubos.Finance.Domain
{
    public interface ITransactionRepository
    {
        Task<QueryBaseResponse<Transaction>> GetTransactionsAsync(TransactionFilter filter);
        Task<Transaction> GetByIdAsync(Guid transactionId);
        Task<Transaction> CreateAsync(Transaction request);

    }

}
