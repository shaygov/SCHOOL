using SmartHomeControl.Patterns.Observer;

namespace SmartHomeControl.Core;

public abstract class SmartDevice : ISubject
{
    private readonly List<IObserver> _observers = [];

    protected SmartDevice(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public bool IsOn { get; protected set; }

    public void Attach(IObserver observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
    }

    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void Notify(string message)
    {
        foreach (var observer in _observers)
        {
            observer.Update(Name, message);
        }
    }

    public virtual void TurnOn()
    {
        if (IsOn)
        {
            Notify("already active.");
            return;
        }

        IsOn = true;
        Notify("activated.");
    }

    public virtual void TurnOff()
    {
        if (!IsOn)
        {
            Notify("already inactive.");
            return;
        }

        IsOn = false;
        Notify("deactivated.");
    }

    public abstract string GetStatus();
}
