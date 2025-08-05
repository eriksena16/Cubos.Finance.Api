using Cubos.Finance.Shared;

namespace Cubos.Finance.Domain
{
    public interface ITransactionRepository
    {
        Task<QueryBaseResponse<Transaction>> GetTransactionsAsync(TransactionFilter filter);
        Task<Transaction> CreateAsync(Transaction request);

    }

}
