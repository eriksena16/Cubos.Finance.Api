using Cubos.Finance.Domain;

namespace Cubos.Finance.Application
{
    public static class AutoMapperCard
    {
        public static Card Map(CardRequest request)
        {
            return new Card
            {
                Id = Guid.NewGuid(),
                Type = request.Type,
                Number = request.Number,
                Cvv = request.Cvv,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }
        //public static CardResponse Map(Card card)
        //{
        //    var lastFour = card.Number.Replace(" ", "").Substring(card.Number.Length - 4);

        //    return new CardResponse(card.Type, lastFour, card.Cvv, card.CreatedAt, card.UpdatedAt)
        //    {
        //        Id = Guid.NewGuid()
        //    };
        //}

    }
}
