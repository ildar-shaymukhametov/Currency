namespace src
{
    public class CurrencyConverter
    {
        private readonly IConvertionRatesProvider rateProvider;

        public CurrencyConverter(IConvertionRatesProvider rateProvider)
        {
            this.rateProvider = rateProvider;
        }

        public Money Convert(Money money, Currency to)
        {
            var rate = rateProvider.GetRate(money.Currency, to);
            return new Money(to, money.Amount * rate);
        }
    }
}