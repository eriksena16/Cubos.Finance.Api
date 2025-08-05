namespace Cubos.Finance.Application
{
    public record TransactionInternalResponse(Guid ReceiverAccountId, decimal Value, string Description, DateTime CreatedAt, DateTime UpdatedAt);
}
