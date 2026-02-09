// ============================================================
// FACTORY METHOD PATTERN - Erstellung verschiedener Benachrichtigungen
// ============================================================
namespace OnlineShopPatterns.Patterns;

public interface INotification
{
    void Send(string recipient, string message);
}

public class EmailNotification : INotification
{
    public void Send(string recipient, string message)
    {
        Console.WriteLine($"  [E-Mail an {recipient}]: {message}");
    }
}

public class SmsNotification : INotification
{
    public void Send(string recipient, string message)
    {
        Console.WriteLine($"  [SMS an {recipient}]: {message}");
    }
}

public class PushNotification : INotification
{
    public void Send(string recipient, string message)
    {
        Console.WriteLine($"  [Push an {recipient}]: {message}");
    }
}

public abstract class NotificationFactory
{
    public abstract INotification CreateNotification();

    public void NotifyCustomer(string recipient, string message)
    {
        var notification = CreateNotification();
        notification.Send(recipient, message);
    }
}

public class EmailNotificationFactory : NotificationFactory
{
    public override INotification CreateNotification() => new EmailNotification();
}

public class SmsNotificationFactory : NotificationFactory
{
    public override INotification CreateNotification() => new SmsNotification();
}

public class PushNotificationFactory : NotificationFactory
{
    public override INotification CreateNotification() => new PushNotification();
}
