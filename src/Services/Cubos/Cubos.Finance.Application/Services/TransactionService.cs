using Cubos.Finance.Domain;
using Cubos.Finance.Shared;
using System.Globalization;

namespace Cubos.Finance.Application
{
    public class TransactionService(INotifier notifier, IUnitOfWork unitOfWork, IBankAccountRepository accountRepository, ITransactionRepository transactionRepository) : ServiceBase(notifier), ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IBankAccountRepository _accountRepository = accountRepository;
        private readonly ITransactionRepository _transactionRepository = transactionRepository;

        public async Task<QueryBaseResponse<TransactionResponse>> GetTransactionsAsync(Guid bankAccountId, TransactionPaginationRequest filterRequest)
        {
            var filter = filterRequest.Map(bankAccountId);

            var transactionsResult = await _transactionRepository.GetTransactionsAsync(filter);

            var response = transactionsResult.MapItems(transaction => transaction.MapToResponse());

            return response;
        }

        public Task<TransactionResponse> RegisterTransactionAsync(Guid bankAccountId, TransactionRequest request)
        {
            return RegisterTransactionBaseAsync(bankAccountId, request, false, t => t.MapToResponse());
        }

        public Task<TransactionInternalResponse> RegisterTransactionInternalAsync(Guid bankAccountId, TransactionRequest request)
        {
            return RegisterTransactionBaseAsync(bankAccountId, request, true, t => t.MapToInternalResponse());
        }

        private async Task<TResponse> RegisterTransactionBaseAsync<TResponse>(Guid bankAccountId, TransactionRequest request, bool isInternal, Func<Transaction, TResponse> mapResponse)
        {
            if (!ValidateTransactionValue(request.Value))
                return default;

            var bankAccount = await _accountRepository.GetAccountByIdAsync(bankAccountId);

            if (bankAccount == null)
            {
                Notify("Conta não encontrada.");
                return default;
            }

            if (isInternal)
            {
                request.Value = request.Value > 0 ? -request.Value : request.Value;
            }

            if (request.Value < 0 && !bankAccount.HasSufficientBalance(request.Value))
            {
                Notify($"Saldo de {bankAccount.Balance.ToString("C", new CultureInfo("pt-BR"))} insuficiente para realizar a transação {request.Value.ToString("C", new CultureInfo("pt-BR"))}.");
                return default;
            }

            var transaction = request.Map(bankAccount.Id);

            bankAccount.ApplyBalance(transaction);

            await _transactionRepository.CreateAsync(transaction);

            await _unitOfWork.CommitAsync();

            return mapResponse(transaction);
        }

        private bool ValidateTransactionValue(decimal value)
        {
            if (value == 0)
            {
                Notify(CubosErrorMessages.INVALID_TRANSACTION_VALUE);
                return false;
            }
            return true;
        }


    }
}

