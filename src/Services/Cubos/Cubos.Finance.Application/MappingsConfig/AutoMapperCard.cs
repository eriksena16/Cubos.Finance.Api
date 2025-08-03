using Cubos.Finance.Domain;
using Cubos.Finance.Shared;

namespace Cubos.Finance.Application
{
    public static class AutoMapperCard
    {
        public static Card Map(this CardRequest request, Guid bankAccountId)
        {
            return new Card
            {
                BankAccountId = bankAccountId,
                Type = request.Type,
                Number = request.Number.FormatCardNumber(),
                Cvv = request.Cvv,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }
        public static CardResponse MapToCreateResponse(this Card card)
        {
            var lastFour = new string(card.Number.Where(char.IsDigit).ToArray())[^4..];

            return new CardResponse(card.Id, card.Type, lastFour, card.Cvv, card.CreatedAt, card.UpdatedAt);
        }
        public static CardResponse MapToGetResponse(this Card card) => new CardResponse(card.Id, card.Type, card.Number, card.Cvv, card.CreatedAt, card.UpdatedAt);
        public static List<CardResponse> MapToResponse(this List<Card> cards) => cards.Select(card => card.MapToGetResponse()).ToList();

    }
}
