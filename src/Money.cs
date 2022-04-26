namespace src
{
    public class Money<T>
    {
        public Money(ICurrency<T> currency, decimal amount)
        {
            Currency = currency;
            Amount = amount;
        }

        // можно сделать их скрытыми
        // и применить implicit operators для прибавления/отнимания
        public ICurrency<T> Currency { get; }
        public decimal Amount { get; }
    }
}