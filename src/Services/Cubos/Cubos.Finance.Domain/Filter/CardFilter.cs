using Cubos.Finance.Shared;


namespace Cubos.Finance.Domain
{
    public class CardFilter : FilterBase<Card>, IQueryObject<Card>
    {
        public Guid? BankAccountId { get; set; }
        public Guid? PeopleId { get; set; }

    }    
}
