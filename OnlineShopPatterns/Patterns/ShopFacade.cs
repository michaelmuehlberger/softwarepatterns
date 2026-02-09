// ============================================================
// FACADE PATTERN - Vereinfachte Schnittstelle fuer den Bestellprozess
// ============================================================
namespace OnlineShopPatterns.Patterns;

public class ShopFacade
{
    private readonly IPaymentProcessor _payment;
    private readonly OrderEventManager _eventManager;
    private readonly PriceCalculator _priceCalculator;
    private readonly NotificationFactory _notificationFactory;
    private readonly Logger _logger;

    public ShopFacade(
        IPaymentProcessor payment,
        OrderEventManager eventManager,
        PriceCalculator priceCalculator,
        NotificationFactory notificationFactory)
    {
        _payment = payment;
        _eventManager = eventManager;
        _priceCalculator = priceCalculator;
        _notificationFactory = notificationFactory;
        _logger = Logger.GetInstance();
    }

    // Eine einzige Methode koordiniert den gesamten Bestellprozess
    public bool PlaceOrder(Order order)
    {
        _logger.Log($"Bestellprozess gestartet fuer {order.CustomerName}");

        // 1. Rabatt berechnen
        Console.WriteLine("\n--- Preisberechnung ---");
        double finalPrice = _priceCalculator.Calculate(order.Total);
        Console.WriteLine($"  Originalpreis: {order.Total:F2} EUR -> Endpreis: {finalPrice:F2} EUR");
        order.Total = finalPrice;

        // 2. Zahlung durchfuehren
        Console.WriteLine("\n--- Zahlung ---");
        bool paymentSuccess = _payment.ProcessPayment(order.CustomerName, order.Total);
        if (!paymentSuccess)
        {
            _logger.Log("Zahlung fehlgeschlagen!");
            return false;
        }
        _logger.Log("Zahlung erfolgreich");

        // 3. Alle Observer benachrichtigen
        Console.WriteLine("\n--- Interne Benachrichtigungen ---");
        _eventManager.NotifyAll(order);

        // 4. Kunden benachrichtigen
        Console.WriteLine("\n--- Kundenbenachrichtigung ---");
        _notificationFactory.NotifyCustomer(
            order.CustomerName,
            $"Ihre Bestellung ({order.Items.Count} Artikel, {order.Total:F2} EUR) wurde bestaetigt!");

        _logger.Log("Bestellprozess abgeschlossen");
        return true;
    }
}
