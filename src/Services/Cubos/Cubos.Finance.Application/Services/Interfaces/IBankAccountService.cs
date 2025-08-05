namespace Cubos.Finance.Application
{
    public interface IBankAccountService
    {
        /// <summary>
        /// Cria uma nova conta bancária para a pessoa informada.
        /// </summary>
        Task<BankAccountResponse> CreateAsync(Guid peopleId, BankAccountRequest request);

        /// <summary>
        /// Retorna todas as contas bancárias vinculadas a uma pessoa.
        /// </summary>
        Task<List<BankAccountResponse>> GetAccountsAsync(Guid peopleId);

        Task<BankAccountBalanceResponse> GetAccountBalanceAsync(Guid accountId);
    }
}
