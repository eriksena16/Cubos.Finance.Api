using Cubos.Finance.Domain;
using Cubos.Finance.External;
using Cubos.Finance.Shared;

namespace Cubos.Finance.Application
{
    public class BankAccountService(INotifier notifier, IUnitOfWork unitOfWork, IBankAccountRepository peopleRepository, IComplianceFacade compliance) : ServiceBase(notifier), IBankAccountService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IBankAccountRepository _accountRepository = peopleRepository;
        private readonly IComplianceFacade _compliance = compliance;

        public async Task<BankAccountResponse> CreateAsync(Guid peopleId, BankAccountRequest request)
        {
            var account = request.Map(peopleId);

            if (await _accountRepository.HasBankAccountAsync(account.Account))
            {
                Notify("Já existe uma conta com este número.");
                return null;
            }
            await _accountRepository.CreateAsync(account);

            await _unitOfWork.CommitAsync();

            return account.MapToResponse();
        }
        public async Task<List<BankAccountResponse>> GetAccountsAsync(Guid peopleId)
        {
            var accounts = (await _accountRepository.GetAccountsAsync(peopleId)).MapToResponse();

            return accounts;
        }
    }
}
