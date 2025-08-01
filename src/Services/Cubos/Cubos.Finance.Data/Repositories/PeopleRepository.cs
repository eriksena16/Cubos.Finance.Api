using Cubos.Finance.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cubos.Finance.Data
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly FinanceContext _context;

        public PeopleRepository(FinanceContext context)
        {
            _context = context;
        }

        public async Task<People> CreateAsync(People request)
        {
            await _context.People.AddAsync(request);

            return request;
        }
        public async Task<bool> HasPeopleAsync(string document)
        {
            return await _context.People
                .AsNoTracking()
                .AnyAsync(people => people.Document.ToLower() == document);
        }
    }
}
