using System;
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
        var rate = GetRandomNumber();
        var amount = GetRandomNumber();

        var sourceCurrency = GetRandomCurrency();
        var targetCurrency = GetRandomCurrency();

        var money = new Money<string>(sourceCurrency, amount);

        var rateProvider = Substitute.For<IRateProvider>();
        rateProvider.GetRate(money.Currency, targetCurrency).Returns(rate);

        var sut = new CurrencyConverter(rateProvider);
        var actualMoney = sut.Convert(money, targetCurrency);

        var expectedCurrency = targetCurrency;
        var expectedAmount = rate * amount;
        Assert.Equal(expectedAmount, actualMoney.Amount);
        Assert.Equal(expectedCurrency, actualMoney.Currency);
    }

    [Fact]
    public void Adds_one_currency_to_another()
    {
        var rateA = GetRandomNumber();
        var rateB = GetRandomNumber();
        var amountA = GetRandomNumber();
        var amountB = GetRandomNumber();

        var currencyA = GetRandomCurrency();
        var currencyB = GetRandomCurrency();
        var targetCurrency = GetRandomCurrency();

        var moneyA = new Money<string>(currencyA, amountA);
        var moneyB = new Money<string>(currencyB, amountB);

        var rateProvider = Substitute.For<IRateProvider>();
        rateProvider.GetRate(currencyA, targetCurrency).Returns(rateA);
        rateProvider.GetRate(currencyB, targetCurrency).Returns(rateB);

        var sut = new CurrencyConverter(rateProvider);
        var actualMoney = sut.Add(moneyA, moneyB, targetCurrency);

        var expectedCurrency = targetCurrency;
        var expectedAmount = rateA * amountA + rateB * amountB;
        Assert.Equal(expectedAmount, actualMoney.Amount);
        Assert.Equal(expectedCurrency, actualMoney.Currency);
    }

    [Fact]
    public void Subtracts_one_currency_from_another()
    {
        var rateA = GetRandomNumber();
        var rateB = GetRandomNumber();
        var amountA = GetRandomNumber();
        var amountB = GetRandomNumber();

        var currencyA = GetRandomCurrency();
        var currencyB = GetRandomCurrency();
        var targetCurrency = GetRandomCurrency();

        var moneyA = new Money<string>(currencyA, amountA);
        var moneyB = new Money<string>(currencyB, amountB);

        var rateProvider = Substitute.For<IRateProvider>();
        rateProvider.GetRate(currencyA, targetCurrency).Returns(rateA);
        rateProvider.GetRate(currencyB, targetCurrency).Returns(rateB);

        var sut = new CurrencyConverter(rateProvider);
        var actualMoney = sut.Subtract(moneyA, moneyB, targetCurrency);

        var expectedCurrency = targetCurrency;
        var expectedAmount = rateA * amountA - rateB * amountB;
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

    private static decimal GetRandomNumber()
    {
        return (decimal)Random.Shared.Next() + (decimal)Random.Shared.NextDouble();
    }
}