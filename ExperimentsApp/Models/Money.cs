namespace ExperimentsApp.Models;

public sealed class Money(decimal amount, string currencyCode)
{
    public decimal Amount { get; } = amount;
    public string CurrencyCode { get; } = currencyCode;

    // Positional patterns use this method.
    public void Deconstruct(out decimal amount, out string currencyCode)
    {
        amount = Amount;
        currencyCode = CurrencyCode;
    }
}