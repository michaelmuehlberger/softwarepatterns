// ============================================================
// ADAPTER PATTERN - Anbindung eines Legacy-Zahlungssystems
// ============================================================
namespace OnlineShopPatterns.Patterns;

// Schnittstelle, die unser Shop erwartet
public interface IPaymentProcessor
{
    bool ProcessPayment(string customer, double amount);
}

// Altes Zahlungssystem mit inkompatibler Schnittstelle
public class LegacyBankSystem
{
    public int MakeTransaction(string accountHolder, int amountInCents, string currency)
    {
        Console.WriteLine($"  [Legacy-Bank] Transaktion: {accountHolder}, {amountInCents} {currency}");
        return 1; // 1 = Erfolg
    }
}

// Adapter uebersetzt zwischen den Schnittstellen
public class LegacyBankAdapter : IPaymentProcessor
{
    private readonly LegacyBankSystem _legacyBank = new();

    public bool ProcessPayment(string customer, double amount)
    {
        int cents = (int)(amount * 100);
        int result = _legacyBank.MakeTransaction(customer, cents, "EUR");
        return result == 1;
    }
}
