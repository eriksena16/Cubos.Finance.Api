using Cubos.Finance.Domain;

namespace Cubos.Finance.Application
{
    public static class AutoMapperTransaction
    {
        public static Transaction Map(this TransactionRequest transactionRequest, Guid bankAccountId) => new() { 
            BankAccountId = bankAccountId,
            Value = transactionRequest.Value,
            Description = transactionRequest.Description,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        public static TransactionResponse MapToResponse(this Transaction transaction) => new
             (transaction.Id, transaction.Value, transaction.Description, transaction.CreatedAt, transaction.UpdatedAt);
        public static TransactionInternalResponse MapToInternalResponse(this Transaction transaction) => new
             (transaction.BankAccountId, transaction.Value, transaction.Description, transaction.CreatedAt, transaction.UpdatedAt);

        public static List<TransactionResponse> MapToResponse(this List<Transaction> transactions) => transactions.Select(transaction => transaction.MapToResponse()).ToList();

    }
}
