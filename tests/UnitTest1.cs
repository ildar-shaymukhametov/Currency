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
        var rateProvider = Substitute.For<IRateProvider>();
        rateProvider.GetRate(money.Currency, targetCurrency).Returns(rate);
        var sut = new CurrencyConverter(rateProvider);

        var actualMoney = sut.Convert(money, targetCurrency);

        var expectedCurrency = Currency.USD;
        var expectedAmount = 1.3M;
        Assert.Equal(expectedAmount, actualMoney.Amount);
        Assert.Equal(expectedCurrency, actualMoney.Currency);
    }

    [Fact]
    public void Adds_one_currency_to_another()
    {
        var rubEurRate = 0.013M;
        var usdEurRate = 0.94M;
        var targetCurrency = Currency.EUR;

        var moneyA = new Money(Currency.RUB, 100M);
        var moneyB = new Money(Currency.USD, 30M);
        var rateProvider = Substitute.For<IRateProvider>();
        rateProvider.GetRate(moneyA.Currency, targetCurrency).Returns(rubEurRate);
        rateProvider.GetRate(moneyB.Currency, targetCurrency).Returns(usdEurRate);
        var sut = new CurrencyConverter(rateProvider);

        var actualMoney = sut.Add(moneyA, moneyB, targetCurrency);

        var expectedCurrency = targetCurrency;
        var expectedAmount = 29.5M;
        Assert.Equal(expectedAmount, actualMoney.Amount);
        Assert.Equal(expectedCurrency, actualMoney.Currency);
    }
}