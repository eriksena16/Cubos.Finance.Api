using Cubos.Finance.Domain;
using Cubos.Finance.Shared;

namespace Cubos.Finance.Application
{
    public interface IBankAccountService
    {
        Task<BankAccountResponse> CreateAsync(Guid peopleId, BankAccountRequest request);
        Task<CardResponse> CreateCardAsync(Guid bankAccountId, CardRequest request);
        Task<List<BankAccountResponse>> GetAccountsAsync(Guid peopleId);
        Task<List<CardResponse>> GetCardsAsync(Guid bankAccountId);
        Task<QueryBaseResponse<CardResponse>> GetCardsFromPeopleAsync(Guid peopleId, CardPaginationRequest request);
    }
}
