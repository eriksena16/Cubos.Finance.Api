using Cubos.Finance.Shared;


namespace Cubos.Finance.Domain
{
    public class CardFilter : IQueryObject<Card>
    {
        public Guid? BankAccountId { get; set; }
        public Guid? PeopleId { get; set; }
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }

    }
}
