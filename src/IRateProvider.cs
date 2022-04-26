namespace src
{
    public interface IRateProvider
    {
        decimal GetRate(Currency from, Currency to);
    }
}