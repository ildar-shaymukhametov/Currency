namespace src
{
    public class CurrencyConverter
    {
        private readonly IRateProvider rateProvider;

        public CurrencyConverter(IRateProvider rateProvider)
        {
            this.rateProvider = rateProvider;
        }

        public Money<TTargetCurrency> Convert<T, TTargetCurrency>(Money<T> money, ICurrency<TTargetCurrency> to)
        {
            var rate = rateProvider.GetRate(money.Currency, to);
            return new Money<TTargetCurrency>(to, money.Amount * rate);
        }

        // public Money Add(Money moneyA, Money moneyB, Currency targetCurrency)
        // {
        //     var rateA = rateProvider.GetRate(moneyA.Currency, targetCurrency);
        //     var rateB = rateProvider.GetRate(moneyB.Currency, targetCurrency);
        //     var result = rateA * moneyA.Amount + rateB * moneyB.Amount;

        //     return new Money(targetCurrency, result);
        // }
    }
}