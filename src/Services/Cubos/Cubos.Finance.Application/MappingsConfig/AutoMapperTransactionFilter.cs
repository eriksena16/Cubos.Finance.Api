using Cubos.Finance.Domain;

namespace Cubos.Finance.Application
{
    public static class AutoMapperTransactionFilter
    {
        public static TransactionFilter Map(this TransactionPaginationRequest request, Guid bankAccountId)
        {
            return new TransactionFilter
            {
                Type = request.Type,
                BankAccountId = bankAccountId,
                ItemsPerPage = request.ItemsPerPage,
                CurrentPage = request.CurrentPage,

            };
        }

    }

}
