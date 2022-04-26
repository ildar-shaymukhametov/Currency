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

        public Money<TTargetCurrency> Add<TMoneyACurrency, TMoneyBCurrency, TTargetCurrency>(Money<TMoneyACurrency> moneyA, Money<TMoneyBCurrency> moneyB, ICurrency<TTargetCurrency> targetCurrency)
        {
            var rateA = rateProvider.GetRate(moneyA.Currency, targetCurrency);
            var rateB = rateProvider.GetRate(moneyB.Currency, targetCurrency);
            var sum = rateA * moneyA.Amount + rateB * moneyB.Amount;

            return new Money<TTargetCurrency>(targetCurrency, sum);
        }
    }
}