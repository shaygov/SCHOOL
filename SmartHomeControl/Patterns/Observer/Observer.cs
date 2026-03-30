using System;

namespace SmartHomeControl.Patterns.Observer;

public interface IObserver
{
    void Update(string source, string message);
}

public interface ISubject
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void Notify(string message);
}

public sealed class ConsoleNotifier : IObserver
{
    public ConsoleNotifier(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public void Update(string source, string message)
    {
        Console.WriteLine($"[{Name}] {source}: {message}");
    }
}
