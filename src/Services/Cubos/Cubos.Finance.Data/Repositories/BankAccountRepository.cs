using Cubos.Finance.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cubos.Finance.Data
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly FinanceContext _context;

        public BankAccountRepository(FinanceContext context)
        {
            _context = context;
        }

        public async Task<List<BankAccount>> GetAccountsAsync(Guid peopleId)
        {
            var query = _context.BankAccount.AsQueryable();

            query = ApplyFilter(query, peopleId);

            return await query.ToListAsync();
        }
        public async Task<BankAccount> GetAccountByIdAsync(Guid accountId)
        {
            var account = await _context.BankAccount
                 .FirstOrDefaultAsync(a => a.Id == accountId);

            return account;
        }
        public async Task<BankAccount> CreateAsync(BankAccount request)
        {
            await _context.BankAccount.AddAsync(request);

            return request;
        }
        public async Task<bool> HasBankAccountAsync(string account)
        {
            return await _context.BankAccount
                .AsNoTracking()
                .AnyAsync(bankAccount => EF.Functions.ILike(bankAccount.Account, account));
        }

        private IQueryable<BankAccount> ApplyFilter(IQueryable<BankAccount> query, Guid peopleId)
        {
            if (peopleId != Guid.Empty)
            {
                query = query.Where(account =>
                    account.PeopleId == peopleId);
            }

            return query;
        }

        public void Update(BankAccount request)
        {
            _context.BankAccount.Update(request);
        }
    }
}
