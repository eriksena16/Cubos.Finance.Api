using Cubos.Finance.Domain;
using Cubos.Finance.Shared;

namespace Cubos.Finance.Application
{
    public static class AutoMapperPeople
    {
        public static People Map(this PeopleRequest peopleRequest) => new() { 
            Name = peopleRequest.Name,
            Document = peopleRequest.Document.CleanDocument(),
            Password = peopleRequest.Password.Hash(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        public static PeopleResponse MapToResponse(this People people) => new
             (people.Id, people.Name, people.Document, people.CreatedAt, people.UpdatedAt);

    }
}
