using Cubos.Finance.Domain;

namespace Cubos.Finance.Application
{
    public static class AutoMapperCardFilter
    {
        public static CardFilter Map(this CardPaginationRequest request, Guid peopleId)
        {
            return new CardFilter
            {
                PeopleId = peopleId,
                ItemsPerPage = request.ItemsPerPage,
                CurrentPage = request.CurrentPage,

            };
        }

    }

}
