using Cubos.Finance.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cubos.Finance.Data
{
    public class CardRepository : ICardRepository
    {
        private readonly FinanceContext _context;

        public CardRepository(FinanceContext context)
        {
            _context = context;
        }

        public async Task<List<Card>> GetCardsAsync(Guid bankAccountId)
        {
           var query =  _context.Card.AsQueryable();

            query = ApplyFilter(query, bankAccountId);

            return await query.ToListAsync();
        }
        public async Task<Card> CreateAsync(Card request)
        {
            await _context.Card.AddAsync(request);

            return request;
        }

        private IQueryable<Card> ApplyFilter(IQueryable<Card> query, Guid bankAccountId)
        {
            if (bankAccountId != Guid.Empty)
            {
                query = query.Where(card =>
                    card.BankAccountId == bankAccountId);               
            }

            return query;
        }
    }
}
