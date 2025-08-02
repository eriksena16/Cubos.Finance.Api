using Cubos.Finance.Shared;
using Newtonsoft.Json;

namespace Cubos.Finance.Domain
{
    public class BankAccount : Entity
    {
        [JsonIgnore]
        public Guid PeopleId { get; set; }
        public string Branch { get; set; }
        public string Account { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [JsonIgnore]
        public People People { get; set; }
        public ICollection<Card> Cards { get; set; }
    }
}
