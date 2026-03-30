using SmartHomeControl.Core;
using SmartHomeControl.Devices;
using SmartHomeControl.Patterns.State;
using SmartHomeControl.Patterns.Strategy;

namespace SmartHomeControl.Patterns.Mediator;

public interface ISmartHomeMediator
{
    string CurrentMode { get; }
    string CurrentStrategy { get; }
    IReadOnlyList<SmartDevice> Devices { get; }
    void HandleDeviceChanged(SmartDevice device);
    void ChangeMode(IHomeState state);
    void SetStrategy(IAutomationStrategy strategy);
    void ApplyStrategy();
}

public sealed class SmartHomeMediator : ISmartHomeMediator, IHomeAutomationActions
{
    public SmartHomeMediator(
        Light livingRoomLight,
        AirConditioner airConditioner,
        AlarmSystem alarmSystem,
        DoorLock frontDoorLock)
    {
        LivingRoomLight = livingRoomLight;
        AirConditioner = airConditioner;
        AlarmSystem = alarmSystem;
        FrontDoorLock = frontDoorLock;
        Devices = [livingRoomLight, airConditioner, alarmSystem, frontDoorLock];
        ActiveStrategy = new EcoModeStrategy();
        ModeContext = new HomeModeContext(new HomeState(), this);
        ModeContext.CurrentState.Apply(ModeContext);
    }

    public Light LivingRoomLight { get; }

    public AirConditioner AirConditioner { get; }

    public AlarmSystem AlarmSystem { get; }

    public DoorLock FrontDoorLock { get; }

    public HomeModeContext ModeContext { get; }

    public IAutomationStrategy ActiveStrategy { get; private set; }

    public string CurrentMode => ModeContext.CurrentState.Name;

    public string CurrentStrategy => ActiveStrategy.Name;

    public IReadOnlyList<SmartDevice> Devices { get; }

    public void HandleDeviceChanged(SmartDevice device)
    {
        if (CurrentMode == "Away" && device == LivingRoomLight && LivingRoomLight.IsOn)
        {
            Console.WriteLine("Mediator: light was switched on while the home is away. Security remains armed.");
        }

        if (device == AlarmSystem && AlarmSystem.IsArmed && !FrontDoorLock.IsLocked)
        {
            Console.WriteLine("Mediator: alarm requires a locked door, locking the front door now.");
            FrontDoorLock.TurnOn();
        }
    }

    public void ChangeMode(IHomeState state)
    {
        ModeContext.TransitionTo(state);
        Console.WriteLine($"Mediator: mode changed to {CurrentMode}.");
    }

    public void SetStrategy(IAutomationStrategy strategy)
    {
        ActiveStrategy = strategy;
        Console.WriteLine($"Mediator: strategy changed to {CurrentStrategy}.");
    }

    public void ApplyStrategy()
    {
        ActiveStrategy.Apply(AirConditioner, LivingRoomLight);
        Console.WriteLine($"Mediator: applied {CurrentStrategy} strategy.");
    }

    public void SetSecurity(bool armed, bool locked)
    {
        if (armed)
        {
            AlarmSystem.TurnOn();
        }
        else
        {
            AlarmSystem.TurnOff();
        }

        if (locked)
        {
            FrontDoorLock.TurnOn();
        }
        else
        {
            FrontDoorLock.TurnOff();
        }
    }

    public void SetLighting(bool enabled, int brightness)
    {
        if (enabled)
        {
            LivingRoomLight.TurnOn();
            LivingRoomLight.SetBrightness(brightness);
            return;
        }

        LivingRoomLight.SetBrightness(brightness);
        LivingRoomLight.TurnOff();
    }
}
