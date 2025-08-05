namespace Cubos.Finance.Application
{
    public class TransactionRequest
    {
        public decimal Value { get; set; }
        public string Description { get; set; }

        public decimal ToNegativeValue()
        {
            return Value > 0 ? -Value : Value;
        }

    }

}
