using Cubos.Finance.Shared;
using Newtonsoft.Json;

namespace Cubos.Finance.Domain
{
    public class Transaction : Entity
    {
        public Guid BankAccountId { get; set; }      
        public decimal Value { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsReverted { get; set; }

        [JsonIgnore]
        public BankAccount BankAccount { get; set; }
    }
}
