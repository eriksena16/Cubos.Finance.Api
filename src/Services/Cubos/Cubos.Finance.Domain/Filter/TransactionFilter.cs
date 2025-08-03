using Cubos.Finance.Shared;
using System.ComponentModel.DataAnnotations;


namespace Cubos.Finance.Domain
{
    public class TransactionFilter : FilterBase<Transaction>, IQueryObject<Transaction>
    {
        [RegularExpression(@"^(debit|credit)$", ErrorMessage = "O tipo deve ser 'debit' ou 'credit'.")]
        public string Type { get; set; }

    }
}
