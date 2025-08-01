using Cubos.Finance.Domain;

namespace Cubos.Finance.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FinanceContext _context;

        public UnitOfWork(FinanceContext context)
        {
            _context = context;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }


    }
}

