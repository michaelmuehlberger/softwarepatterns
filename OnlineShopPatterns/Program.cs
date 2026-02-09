using OnlineShopPatterns.Patterns;

Console.WriteLine("========================================================");
Console.WriteLine("  ONLINE-SHOP BESTELLSYSTEM - Design Patterns Demo");
Console.WriteLine("========================================================\n");

// ---------------------------------------------------------------
// 1. SINGLETON: Logger-Instanz (nur eine im gesamten Programm)
// ---------------------------------------------------------------
var logger = Logger.GetInstance();
logger.Log("Shop-System gestartet");

// ---------------------------------------------------------------
// 2. DECORATOR: Produkte mit optionalen Zusatzleistungen
// ---------------------------------------------------------------
Console.WriteLine("\n=== Produkte konfigurieren (Decorator) ===");

IProduct laptop = new BasicLaptop();
laptop = new WarrantyDecorator(laptop);      // + Garantie
laptop = new InsuranceDecorator(laptop);      // + Versicherung
Console.WriteLine($"  Produkt 1: {laptop.GetDescription()} -> {laptop.GetPrice():F2} EUR");

IProduct phone = new BasicPhone();
phone = new PremiumSupportDecorator(phone);   // + Support
Console.WriteLine($"  Produkt 2: {phone.GetDescription()} -> {phone.GetPrice():F2} EUR");

// ---------------------------------------------------------------
// 3. BUILDER: Bestellung schrittweise zusammenbauen
// ---------------------------------------------------------------
Console.WriteLine("\n=== Bestellung aufbauen (Builder) ===");

var order = new OrderBuilder()
    .SetCustomer("Max Mustermann", "Musterstrasse 1, Wien")
    .AddItem(laptop.GetDescription(), laptop.GetPrice())
    .AddItem(phone.GetDescription(), phone.GetPrice())
    .SetPayment("Kreditkarte")
    .AddGiftWrapping()
    .AddExpressShipping()
    .Build();

Console.WriteLine($"  {order}");

// ---------------------------------------------------------------
// 4. STRATEGY: Rabattstrategie waehlen
// ---------------------------------------------------------------
Console.WriteLine("\n=== Rabattstrategie waehlen (Strategy) ===");

var calculator = new PriceCalculator(new PercentageDiscount(10));

// ---------------------------------------------------------------
// 5. ADAPTER: Legacy-Zahlungssystem anbinden
// ---------------------------------------------------------------
IPaymentProcessor payment = new LegacyBankAdapter();

// ---------------------------------------------------------------
// 6. OBSERVER: Abteilungen registrieren sich fuer Benachrichtigungen
// ---------------------------------------------------------------
Console.WriteLine("\n=== Observer registrieren ===");

var eventManager = new OrderEventManager();
eventManager.Subscribe(new WarehouseObserver());
eventManager.Subscribe(new AccountingObserver());
eventManager.Subscribe(new AnalyticsObserver());
Console.WriteLine("  Lager, Buchhaltung und Analytik registriert.");

// ---------------------------------------------------------------
// 7. FACTORY METHOD: Benachrichtigungskanal erstellen
// ---------------------------------------------------------------
NotificationFactory notificationFactory = new EmailNotificationFactory();

// ---------------------------------------------------------------
// 8. FACADE: Gesamter Bestellprozess ueber eine einfache Schnittstelle
// ---------------------------------------------------------------
Console.WriteLine("\n=== Bestellung ausfuehren (Facade) ===");

var shop = new ShopFacade(payment, eventManager, calculator, notificationFactory);
bool success = shop.PlaceOrder(order);

Console.WriteLine($"\n=== Ergebnis: Bestellung {(success ? "erfolgreich" : "fehlgeschlagen")} ===");

// ---------------------------------------------------------------
// Zweite Bestellung mit anderer Strategy und Factory
// ---------------------------------------------------------------
Console.WriteLine("\n\n========================================================");
Console.WriteLine("  ZWEITE BESTELLUNG - andere Patterns-Konfiguration");
Console.WriteLine("========================================================\n");

// Anderes Produkt mit anderem Decorator
IProduct tablet = new BasicLaptop();
tablet = new PremiumSupportDecorator(tablet);
Console.WriteLine($"  Produkt: {tablet.GetDescription()} -> {tablet.GetPrice():F2} EUR");

// Builder: Einfachere Bestellung
var order2 = new OrderBuilder()
    .SetCustomer("Anna Beispiel", "Beispielweg 5, Graz")
    .AddItem(tablet.GetDescription(), tablet.GetPrice())
    .SetPayment("PayPal")
    .Build();

Console.WriteLine($"  {order2}");

// Strategy wechseln: Fixer Rabatt statt Prozent
calculator.SetStrategy(new FixedDiscount(50.00));

// Factory wechseln: SMS statt E-Mail
NotificationFactory smsFactory = new SmsNotificationFactory();

var shop2 = new ShopFacade(payment, eventManager, calculator, smsFactory);
Console.WriteLine("\n=== Bestellung ausfuehren (Facade) ===");
shop2.PlaceOrder(order2);

// ---------------------------------------------------------------
// Singleton: Gesamtes Log ausgeben (beweist: nur eine Instanz)
// ---------------------------------------------------------------
var logger2 = Logger.GetInstance();
logger2.PrintAll();

Console.WriteLine($"\nLogger-Instanzen identisch? {ReferenceEquals(logger, logger2)}");
