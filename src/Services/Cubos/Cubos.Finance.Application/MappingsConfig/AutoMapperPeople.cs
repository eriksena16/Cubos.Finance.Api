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
    public static class AutoMapperTransaction
    {
        public static Transaction Map(this TransactionRequest transactionRequest, Guid bankAccountId) => new() { 
            AccountId = bankAccountId,
            Value = transactionRequest.Value,
            Description = transactionRequest.Description,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        public static TransactionResponse MapToResponse(this Transaction transaction) => new
             (transaction.Id, transaction.Value, transaction.Description, transaction.CreatedAt, transaction.UpdatedAt);

    }
}
