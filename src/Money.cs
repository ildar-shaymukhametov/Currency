namespace src
{
    public class Money
    {
        public Money(Currency currency, decimal amount)
        {
            Amount = amount;
            Currency = currency;
        }

        public decimal Amount { get; }
        public Currency Currency { get; }
    }
}