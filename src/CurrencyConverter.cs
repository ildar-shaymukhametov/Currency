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

        public Money Add(Money moneyA, Money moneyB, Currency targetCurrency)
        {
            var rateA = rateProvider.GetRate(moneyA.Currency, targetCurrency);
            var rateB = rateProvider.GetRate(moneyB.Currency, targetCurrency);
            var result = rateA * moneyA.Amount + rateB * moneyB.Amount;

            return new Money(targetCurrency, result);
        }
    }
}