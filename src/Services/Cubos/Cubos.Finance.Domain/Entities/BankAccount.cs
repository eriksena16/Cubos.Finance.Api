using Cubos.Finance.Shared;
using Newtonsoft.Json;

namespace Cubos.Finance.Domain
{
    public class BankAccount : Entity
    {
        [JsonIgnore]
        public Guid PeopleId { get; set; }
        public string Branch { get; set; }
        public string Account { get; set; }
        public decimal Balance { get; private set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [JsonIgnore]
        public People People { get; set; }

        [JsonIgnore]
        public ICollection<Card> Cards { get; set; } = new List<Card>();

        [JsonIgnore]
        public ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();

        /// <summary>
        /// Aplica uma transação ao saldo e atualiza o estado da conta
        /// </summary>
        public void ApplyBalance(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));

            Balance += transaction.Value;
        }

        /// <summary>
        /// Verifica se o saldo atual suporta a transação com o valor informado
        /// </summary>
        public bool HasSufficientBalance(decimal value)
        {
            return Balance + value >= 0;
        }
    }

}
