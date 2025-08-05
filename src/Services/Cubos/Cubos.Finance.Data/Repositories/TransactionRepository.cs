using Cubos.Finance.Domain;
using Cubos.Finance.Shared;

namespace Cubos.Finance.Data
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly FinanceContext _context;

        public TransactionRepository(FinanceContext context)
        {
            _context = context;
        }

        public async Task<QueryBaseResponse<Transaction>> GetTransactionsAsync(TransactionFilter filter)
        {
            var query = _context.Transaction.AsQueryable();

            query = ApplyFilter(query, filter);

            var result = await query.ResponseAsync(filter);

            return result;
        }

        public async Task<Transaction> CreateAsync(Transaction request)
        {
            await _context.Transaction.AddAsync(request);

            return request;
        }

        private IQueryable<Transaction> ApplyFilter(IQueryable<Transaction> query, TransactionFilter filter)
        {
            if (filter.BankAccountId.HasValue)
            {
                query = query.Where(transaction =>
                    transaction.BankAccountId == filter.BankAccountId);
            }
            if (!string.IsNullOrEmpty(filter.Type))
            {
                if(string.Equals(filter.Type, "debit", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(transaction => transaction.Value < 0);
                }
                else if(string.Equals(filter.Type, "credit", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(transaction => transaction.Value > 0);
                }
            }

            return query;
        }
    }

}
