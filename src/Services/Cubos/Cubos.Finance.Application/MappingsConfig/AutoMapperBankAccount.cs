using Cubos.Finance.Domain;

namespace Cubos.Finance.Application
{
    public static class AutoMapperBankAccount
    {
        public static BankAccount Map(this BankAccountRequest bankAccountRequest, Guid peopleId) => new() { 
            PeopleId = peopleId,
            Branch = bankAccountRequest.Branch,
            Account = bankAccountRequest.Account,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        public static BankAccountResponse MapToResponse(this BankAccount bankAccount) => new
             (bankAccount.Id, bankAccount.Branch, bankAccount.Account, bankAccount.CreatedAt, bankAccount.UpdatedAt);
        public static BankAccountBalanceResponse MapToBalanceResponse(this BankAccount bankAccount) => new
             (bankAccount.Balance);
        public static List<BankAccountResponse> MapToResponse(this List<BankAccount> bankAccounts) => bankAccounts.Select(account=> account.MapToResponse()).ToList();

    }
}
