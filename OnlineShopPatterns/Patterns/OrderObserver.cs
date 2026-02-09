// ============================================================
// OBSERVER PATTERN - Benachrichtigung bei neuen Bestellungen
// ============================================================
namespace OnlineShopPatterns.Patterns;

public interface IOrderObserver
{
    void OnOrderPlaced(Order order);
}

// Beobachter: Lager wird informiert
public class WarehouseObserver : IOrderObserver
{
    public void OnOrderPlaced(Order order)
    {
        Console.WriteLine($"  [Lager] Kommissionierung gestartet fuer: {order.CustomerName}");
    }
}

// Beobachter: Buchhaltung wird informiert
public class AccountingObserver : IOrderObserver
{
    public void OnOrderPlaced(Order order)
    {
        Console.WriteLine($"  [Buchhaltung] Rechnung erstellt: {order.Total:F2} EUR");
    }
}

// Beobachter: Statistik wird aktualisiert
public class AnalyticsObserver : IOrderObserver
{
    public void OnOrderPlaced(Order order)
    {
        Console.WriteLine($"  [Analytik] Bestellung erfasst: {order.Items.Count} Artikel");
    }
}

// Subject: Verwaltet und benachrichtigt Observer
public class OrderEventManager
{
    private readonly List<IOrderObserver> _observers = new();

    public void Subscribe(IOrderObserver observer) => _observers.Add(observer);

    public void NotifyAll(Order order)
    {
        foreach (var observer in _observers)
            observer.OnOrderPlaced(order);
    }
}
