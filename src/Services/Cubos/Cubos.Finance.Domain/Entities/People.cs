using Cubos.Finance.Shared;

namespace Cubos.Finance.Domain
{
    public class People : Entity
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt => DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }
}
