using Cubos.Finance.Shared;
using System.ComponentModel.DataAnnotations;

namespace Cubos.Finance.Application
{
    public class TransactionPaginationRequest : FilterBase<TransactionRequest>, IQueryObject<TransactionRequest>
    {
        [RegularExpression(@"^(debit|credit)$", ErrorMessage = "O tipo deve ser 'debit' ou 'credit'.")]
        public string Type { get; set; }

    }
}
