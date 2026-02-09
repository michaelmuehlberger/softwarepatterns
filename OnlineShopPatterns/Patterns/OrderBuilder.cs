// ============================================================
// BUILDER PATTERN - Schrittweiser Aufbau einer Bestellung
// ============================================================
namespace OnlineShopPatterns.Patterns;

public class Order
{
    public string CustomerName { get; set; } = "";
    public string Address { get; set; } = "";
    public List<string> Items { get; set; } = new();
    public double Total { get; set; }
    public string PaymentMethod { get; set; } = "";
    public bool GiftWrapping { get; set; }
    public bool ExpressShipping { get; set; }

    public override string ToString()
    {
        var items = string.Join(", ", Items);
        return $"Bestellung von {CustomerName}: [{items}] = {Total:F2} EUR" +
               $" | Zahlung: {PaymentMethod}" +
               (GiftWrapping ? " | Geschenkverpackung" : "") +
               (ExpressShipping ? " | Express-Versand" : "");
    }
}

public class OrderBuilder
{
    private readonly Order _order = new();

    public OrderBuilder SetCustomer(string name, string address)
    {
        _order.CustomerName = name;
        _order.Address = address;
        return this;
    }

    public OrderBuilder AddItem(string item, double price)
    {
        _order.Items.Add(item);
        _order.Total += price;
        return this;
    }

    public OrderBuilder SetPayment(string method)
    {
        _order.PaymentMethod = method;
        return this;
    }

    public OrderBuilder AddGiftWrapping()
    {
        _order.GiftWrapping = true;
        _order.Total += 3.50;
        return this;
    }

    public OrderBuilder AddExpressShipping()
    {
        _order.ExpressShipping = true;
        _order.Total += 9.99;
        return this;
    }

    public Order Build() => _order;
}
