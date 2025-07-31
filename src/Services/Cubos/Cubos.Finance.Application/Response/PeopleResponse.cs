using Cubos.Finance.Shared;

namespace Cubos.Finance.Application
{
    public class PeopleResponse : Entity
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
