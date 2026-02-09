// ============================================================
// DECORATOR PATTERN - Dynamische Erweiterung von Produkten
// ============================================================
namespace OnlineShopPatterns.Patterns;

public interface IProduct
{
    string GetDescription();
    double GetPrice();
}

public class BasicLaptop : IProduct
{
    public string GetDescription() => "Laptop (Basis)";
    public double GetPrice() => 799.00;
}

public class BasicPhone : IProduct
{
    public string GetDescription() => "Smartphone (Basis)";
    public double GetPrice() => 499.00;
}

// Decorator: Garantieverlaengerung
public class WarrantyDecorator : IProduct
{
    private readonly IProduct _product;
    public WarrantyDecorator(IProduct product) { _product = product; }
    public string GetDescription() => _product.GetDescription() + " + 3 Jahre Garantie";
    public double GetPrice() => _product.GetPrice() + 89.00;
}

// Decorator: Versicherung
public class InsuranceDecorator : IProduct
{
    private readonly IProduct _product;
    public InsuranceDecorator(IProduct product) { _product = product; }
    public string GetDescription() => _product.GetDescription() + " + Versicherung";
    public double GetPrice() => _product.GetPrice() + 49.99;
}

// Decorator: Premium-Support
public class PremiumSupportDecorator : IProduct
{
    private readonly IProduct _product;
    public PremiumSupportDecorator(IProduct product) { _product = product; }
    public string GetDescription() => _product.GetDescription() + " + Premium-Support";
    public double GetPrice() => _product.GetPrice() + 29.99;
}
