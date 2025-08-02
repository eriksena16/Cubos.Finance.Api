using Cubos.Finance.Domain;

namespace Cubos.Finance.Application
{
    public interface IBankAccountService
    {
        Task<BankAccountResponse> CreateAsync(Guid peopleId, BankAccountRequest request);
        Task<List<BankAccountResponse>> GetAccountsAsync(Guid peopleId);
    }
}
