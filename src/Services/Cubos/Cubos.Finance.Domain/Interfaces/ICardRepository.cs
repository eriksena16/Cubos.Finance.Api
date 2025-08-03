using Cubos.Finance.Shared;

namespace Cubos.Finance.Domain
{
    public interface ICardRepository 
    {
        Task<List<Card>> GetCardsAsync(Guid bankAccountId);
        Task<QueryBaseResponse<Card>> GetCardsFromPeopleAsync(CardFilter filter);
        Task<Card> CreateAsync(Card request);
        Task<bool> HasCardPhysicalAsync(Guid bankAccountId);
        Task<bool> HasCardAsync(string number);
    }

}
