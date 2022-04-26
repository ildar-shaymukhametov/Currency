namespace src
{
    public interface IConvertionRatesProvider
    {
        decimal GetRate(Currency from, Currency to);
    }
}