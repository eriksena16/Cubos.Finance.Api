using Cubos.Finance.Domain;
using Cubos.Finance.Shared;
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
            var filter = new CardFilter
            {
                BankAccountId = bankAccountId
            };

            var query = _context.Card.AsQueryable();

            query = ApplyFilter(query, filter);

            return await query.ToListAsync();
        }
        public async Task<QueryBaseResponse<Card>> GetCardsFromPeopleAsync(CardFilter filter)
        {
            var query = _context.Card.AsQueryable();

            query = ApplyFilter(query, filter);

            var result = await query.ResponseAsync(filter);

            return result;
        }

        public async Task<bool> HasCardPhysicalAsync(Guid bankAccountId)
        {
            return await _context.Card
                .AsNoTracking()
                .AnyAsync(card => card.BankAccountId == bankAccountId
                        && EF.Functions.ILike(card.Type, "physical"));
        }
        public async Task<bool> HasCardAsync(string number)
        {
            return await _context.Card
                .AsNoTracking()
                .AnyAsync(card => EF.Functions.ILike(card.Number, number));
        }
        public async Task<Card> CreateAsync(Card request)
        {
            await _context.Card.AddAsync(request);

            return request;
        }

        private IQueryable<Card> ApplyFilter(IQueryable<Card> query, CardFilter filter)
        {
            if (filter.BankAccountId.HasValue)
            {
                query = query.Where(card =>
                    card.BankAccountId == filter.BankAccountId);
            }
            if (filter.PeopleId.HasValue)
            {
                query = query.Where(card =>
                    card.BankAccount.PeopleId == filter.PeopleId);
            }

            return query;
        }
    }

}
