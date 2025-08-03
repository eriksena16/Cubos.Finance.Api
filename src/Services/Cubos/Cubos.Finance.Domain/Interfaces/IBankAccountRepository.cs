namespace Cubos.Finance.Domain
{
    public interface IBankAccountRepository
    {
        Task<BankAccount> GetAccountByIdAsync(Guid accountId);
        Task<List<BankAccount>> GetAccountsAsync(Guid peopleId);
        Task<BankAccount> CreateAsync(BankAccount request);
        Task<bool> HasBankAccountAsync(string account);
    }
}
