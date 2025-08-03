namespace Cubos.Finance.Application
{
    public record TransactionResponse(Guid Id, decimal Value, string Description, DateTime CreatedAt, DateTime UpdatedAt);
}
