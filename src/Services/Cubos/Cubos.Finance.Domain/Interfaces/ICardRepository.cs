namespace Cubos.Finance.Domain
{
    public interface ICardRepository 
    {
        Task<List<Card>> GetCardsAsync(Guid bankAccountId);
        Task<Card> CreateAsync(Card request);
    }

}
