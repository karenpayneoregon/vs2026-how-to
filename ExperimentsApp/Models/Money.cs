namespace ExperimentsApp.Models;

public sealed class Money
{
    public decimal Amount { get; }
    public string CurrencyCode { get; }

    public Money(decimal amount, string currencyCode)
    {
        Amount = amount;
        CurrencyCode = currencyCode;
    }

    // Positional patterns use this method.
    public void Deconstruct(out decimal amount, out string currencyCode)
    {
        amount = Amount;
        currencyCode = CurrencyCode;
    }
}