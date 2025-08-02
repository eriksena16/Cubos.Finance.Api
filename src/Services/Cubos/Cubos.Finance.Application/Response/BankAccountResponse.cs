namespace Cubos.Finance.Application
{
    public record BankAccountResponse(Guid Id, string Branch, string Account, DateTime CreatedAt, DateTime UpdatedAt);
}
