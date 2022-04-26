namespace src
{
    public class Money<T>
    {
        public Money(ICurrency<T> currency, decimal amount)
        {
            Currency = currency;
            Amount = amount;
        }

        public ICurrency<T> Currency { get; }
        public decimal Amount { get; }
    }
}