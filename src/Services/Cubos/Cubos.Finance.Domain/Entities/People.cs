using Cubos.Finance.Shared;
using Newtonsoft.Json;

namespace Cubos.Finance.Domain
{
    public class People : Entity
    {
        public string Name { get; set; }
        public string Document { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public DateTime CreatedAt => DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }
}
