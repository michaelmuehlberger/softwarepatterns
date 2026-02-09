// ============================================================
// STRATEGY PATTERN - Verschiedene Rabattstrategien
// ============================================================
namespace OnlineShopPatterns.Patterns;

public interface IDiscountStrategy
{
    double ApplyDiscount(double total);
    string GetName();
}

public class NoDiscount : IDiscountStrategy
{
    public double ApplyDiscount(double total) => total;
    public string GetName() => "Kein Rabatt";
}

public class PercentageDiscount : IDiscountStrategy
{
    private readonly int _percent;
    public PercentageDiscount(int percent) { _percent = percent; }
    public double ApplyDiscount(double total) => total * (1 - _percent / 100.0);
    public string GetName() => $"{_percent}% Rabatt";
}

public class FixedDiscount : IDiscountStrategy
{
    private readonly double _amount;
    public FixedDiscount(double amount) { _amount = amount; }
    public double ApplyDiscount(double total) => Math.Max(0, total - _amount);
    public string GetName() => $"{_amount:F2} EUR Rabatt";
}

public class PriceCalculator
{
    private IDiscountStrategy _strategy;

    public PriceCalculator(IDiscountStrategy strategy)
    {
        _strategy = strategy;
    }

    public void SetStrategy(IDiscountStrategy strategy)
    {
        _strategy = strategy;
    }

    public double Calculate(double total)
    {
        Console.WriteLine($"  Angewendet: {_strategy.GetName()}");
        return _strategy.ApplyDiscount(total);
    }
}
