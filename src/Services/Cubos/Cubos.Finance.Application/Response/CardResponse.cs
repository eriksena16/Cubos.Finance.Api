namespace Cubos.Finance.Application
{
    public record CardResponse(Guid Id, string Type, string Number, string Cvv, DateTime CreatedAt, DateTime UpdatedAt);
}
