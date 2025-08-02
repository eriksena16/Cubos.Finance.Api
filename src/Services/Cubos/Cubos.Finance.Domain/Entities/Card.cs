using Cubos.Finance.Shared;
using Newtonsoft.Json;

namespace Cubos.Finance.Domain
{
    public class Card : Entity
    {
        public Guid BankAccountId { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
        public string Cvv { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [JsonIgnore]
        public BankAccount BankAccount { get; set; }
    }
}
