using NSubstitute;
using src;
using Xunit;

namespace tests;

public class UnitTest1
{
    [Fact]
    public void Converts_one_currency_to_another()
    {
        var rate = 0.013M;
        var targetCurrency = Currency.USD;

        var money = new Money(Currency.RUB, 100M);
        var rateProvider = Substitute.For<IConvertionRatesProvider>();
        rateProvider.GetRate(money.Currency, targetCurrency).Returns(rate);
        var sut = new CurrencyConverter(rateProvider);

        var actualMoney = sut.Convert(money, targetCurrency);

        var expectedCurrency = Currency.USD;
        var expectedAmount = 1.3M;
        Assert.Equal(expectedAmount, actualMoney.Amount);
        Assert.Equal(expectedCurrency, actualMoney.Currency);
    }
}