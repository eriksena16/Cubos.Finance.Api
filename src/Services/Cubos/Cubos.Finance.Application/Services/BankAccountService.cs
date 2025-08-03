using Cubos.Finance.Domain;
using Cubos.Finance.External;
using Cubos.Finance.Shared;

namespace Cubos.Finance.Application
{
    public class BankAccountService(INotifier notifier, IUnitOfWork unitOfWork, IBankAccountRepository peopleRepository, ICardRepository cardRepository, IComplianceFacade compliance) : ServiceBase(notifier), IBankAccountService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IBankAccountRepository _accountRepository = peopleRepository;
        private readonly ICardRepository _cardRepository = cardRepository;
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
        public async Task<CardResponse> CreateCardAsync(Guid bankAccountId, CardRequest request)
        {
            if (!request.Number.IsValidCardNumber())
            {
                Notify("Número do cartão inválido.");
                return null;
            }

            var card = request.Map(bankAccountId);

            if (await _cardRepository.HasCardAsync(card.Number))
            {
                Notify("Já existe um cartão com este número.");
                return null;
            }
            if (card.Type.ToLower() == "physical")
            {
                var alreadyHasPhysical = await _cardRepository.HasCardPhysicalAsync(bankAccountId);
                if (alreadyHasPhysical)
                {
                    Notify("Já existe um cartão físico vinculado a esta conta.");
                    return null;
                }

            }
            await _cardRepository.CreateAsync(card);

            await _unitOfWork.CommitAsync();

            return card.MapToCreateResponse();
        }
        public async Task<TransactionResponse> CreateTransactionAsync(Guid bankAccountId, TransactionRequest request)
        {
            var account = await _accountRepository.GetAccountByIdAsync(bankAccountId);

            if (account == null)
            {
                Notify("Conta não encontrada.");
                return null;
            }

            if (request.Value < 0 && account.Balance + request.Value < 0)
            {
                Notify("Saldo insuficiente para realizar a transação.");
                return null;
            }

            var transaction = request.Map(bankAccountId);

            account.Balance += request.Value;
            account.UpdatedAt = DateTime.UtcNow;

            await _cardRepository.CreateAsync(card);

            await _unitOfWork.CommitAsync();

            return card.MapToCreateResponse();
        }
        public async Task<List<BankAccountResponse>> GetAccountsAsync(Guid peopleId)
        {
            var accounts = (await _accountRepository.GetAccountsAsync(peopleId)).MapToResponse();

            return accounts;
        }
        public async Task<List<CardResponse>> GetCardsAsync(Guid bankAccountId)
        {
            var cards = (await _cardRepository.GetCardsAsync(bankAccountId)).MapToResponse();

            return cards;
        }
        public async Task<QueryBaseResponse<CardResponse>> GetCardsFromPeopleAsync(Guid peopleId, CardPaginationRequest request)
        {
            var filter = request.Map(peopleId);

            var cardsResult = await _cardRepository.GetCardsFromPeopleAsync(filter);

            var response = cardsResult.MapItems(card => card.MapToGetResponse());

            return response;
        }
    }
}
