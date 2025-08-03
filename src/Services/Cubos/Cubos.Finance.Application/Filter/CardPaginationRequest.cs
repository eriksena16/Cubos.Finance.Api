using Cubos.Finance.Shared;

namespace Cubos.Finance.Application
{
    public class CardPaginationRequest : IQueryObject<CardPaginationRequest>
    {
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
