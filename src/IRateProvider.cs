namespace src
{
    public interface IRateProvider
    {
        decimal GetRate<TSourceCurrency, TTargetCurrency>(ICurrency<TSourceCurrency> from, ICurrency<TTargetCurrency> to);
    }
}