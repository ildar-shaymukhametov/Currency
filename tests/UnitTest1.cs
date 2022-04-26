using System.IO;
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

        var sourceCurrency = GetRandomCurrency();
        var targetCurrency = GetRandomCurrency();

        var money = new Money<string>(sourceCurrency, 100M);

        var rateProvider = Substitute.For<IRateProvider>();
        rateProvider.GetRate(money.Currency, targetCurrency).Returns(rate);

        var sut = new CurrencyConverter(rateProvider);
        var actualMoney = sut.Convert(money, targetCurrency);

        var expectedCurrency = targetCurrency;
        var expectedAmount = 1.3M;
        Assert.Equal(expectedAmount, actualMoney.Amount);
        Assert.Equal(expectedCurrency, actualMoney.Currency);
    }

    [Fact]
    public void Adds_one_currency_to_another()
    {
        var rateA = 0.013M;
        var rateB = 0.94M;

        var currencyA = GetRandomCurrency();
        var currencyB = GetRandomCurrency();
        var targetCurrency = GetRandomCurrency();

        var moneyA = new Money<string>(currencyA, 100M);
        var moneyB = new Money<string>(currencyB, 30M);

        var rateProvider = Substitute.For<IRateProvider>();
        rateProvider.GetRate(currencyA, targetCurrency).Returns(rateA);
        rateProvider.GetRate(currencyB, targetCurrency).Returns(rateB);

        var sut = new CurrencyConverter(rateProvider);
        var actualMoney = sut.Add(moneyA, moneyB, targetCurrency);

        var expectedCurrency = targetCurrency;
        var expectedAmount = 29.5M;
        Assert.Equal(expectedAmount, actualMoney.Amount);
        Assert.Equal(expectedCurrency, actualMoney.Currency);
    }

    private static ICurrency<string> GetRandomCurrency()
    {
        var result = Substitute.For<ICurrency<string>>();
        result.Value.Returns(GetRandomString());

        return result;
    }

    private static string GetRandomString()
    {
        return Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
    }
}