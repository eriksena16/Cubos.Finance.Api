using Cubos.Finance.Shared;
using Newtonsoft.Json;
using System.Security.Principal;

namespace Cubos.Finance.Domain
{
    public class People : Entity
    {
        public string Name { get; set; }
        public string Document { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
    public class Transaction : Entity
    {
        public Guid BankAccountId { get; set; }      
        public decimal Value { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [JsonIgnore]
        public BankAccount BankAccount { get; set; }
    }
}
