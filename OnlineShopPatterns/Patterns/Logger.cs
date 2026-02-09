// ============================================================
// SINGLETON PATTERN - Zentraler Logger
// ============================================================
namespace OnlineShopPatterns.Patterns;

public class Logger
{
    private static Logger? _instance;
    private readonly List<string> _logs = new();

    private Logger() { }

    public static Logger GetInstance()
    {
        if (_instance == null)
            _instance = new Logger();
        return _instance;
    }

    public void Log(string message)
    {
        var entry = $"[{DateTime.Now:HH:mm:ss}] {message}";
        _logs.Add(entry);
        Console.WriteLine(entry);
    }

    public void PrintAll()
    {
        Console.WriteLine("\n=== Gesamtes Log ===");
        foreach (var log in _logs)
            Console.WriteLine(log);
    }
}
