using SmartHomeControl.Core;
using SmartHomeControl.Devices;
using SmartHomeControl.Patterns.Command;
using SmartHomeControl.Patterns.Mediator;
using SmartHomeControl.Patterns.Observer;
using SmartHomeControl.Patterns.State;
using SmartHomeControl.Patterns.Strategy;

namespace SmartHomeControl;

public sealed class SmartHomeApp
{
    private readonly CommandInvoker _invoker = new();
    private readonly SmartHomeMediator _mediator;
    private readonly Light _light;
    private readonly AirConditioner _airConditioner;
    private readonly AlarmSystem _alarmSystem;
    private readonly DoorLock _doorLock;

    public SmartHomeApp()
    {
        _light = new Light("Living room light");
        _airConditioner = new AirConditioner("Air conditioner");
        _alarmSystem = new AlarmSystem("Alarm system");
        _doorLock = new DoorLock("Front door lock");
        _mediator = new SmartHomeMediator(_light, _airConditioner, _alarmSystem, _doorLock);

        var ownerDisplay = new ConsoleNotifier("Owner app");
        var securityDisplay = new ConsoleNotifier("Security panel");

        foreach (var device in new SmartDevice[] { _light, _airConditioner, _alarmSystem, _doorLock })
        {
            device.Attach(ownerDisplay);
            device.Attach(securityDisplay);
        }
    }

    public bool HandleMenuSelection(string input)
    {
        switch (input)
        {
            case "1":
                _invoker.Execute(new TurnOnCommand(_light, _mediator));
                break;
            case "2":
                _invoker.Execute(new TurnOffCommand(_light, _mediator));
                break;
            case "3":
                _invoker.Execute(new TurnOnCommand(_airConditioner, _mediator));
                break;
            case "4":
                _invoker.Execute(new TurnOffCommand(_airConditioner, _mediator));
                break;
            case "5":
                _invoker.Execute(new ChangeModeCommand(_mediator, new HomeState()));
                break;
            case "6":
                _invoker.Execute(new ChangeModeCommand(_mediator, new AwayState()));
                break;
            case "7":
                _invoker.Execute(new ChangeModeCommand(_mediator, new NightState()));
                break;
            case "8":
                _invoker.Execute(new ChangeStrategyCommand(_mediator, new EcoModeStrategy()));
                break;
            case "9":
                _invoker.Execute(new ChangeStrategyCommand(_mediator, new ComfortModeStrategy()));
                break;
            case "10":
                _invoker.Execute(new ApplyStrategyCommand(_mediator));
                break;
            case "11":
                RunDemoScenario();
                break;
            case "12":
                ShowStatus();
                break;
            case "13":
                ShowHistory();
                break;
            case "0":
                return false;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }

        return true;
    }

    public void PrintMenu()
    {
        Console.WriteLine();
        Console.WriteLine("=== Smart Home Control ===");
        Console.WriteLine($"Current mode: {_mediator.CurrentMode}");
        Console.WriteLine($"Active strategy: {_mediator.CurrentStrategy}");
        Console.WriteLine("1. Turn on light");
        Console.WriteLine("2. Turn off light");
        Console.WriteLine("3. Turn on air conditioner");
        Console.WriteLine("4. Turn off air conditioner");
        Console.WriteLine("5. Switch mode to Home");
        Console.WriteLine("6. Switch mode to Away");
        Console.WriteLine("7. Switch mode to Night");
        Console.WriteLine("8. Select Eco strategy");
        Console.WriteLine("9. Select Comfort strategy");
        Console.WriteLine("10. Apply current strategy");
        Console.WriteLine("11. Run demo scenario");
        Console.WriteLine("12. Show device status");
        Console.WriteLine("13. Show command history");
        Console.WriteLine("0. Exit");
        Console.Write("Choose an option: ");
    }

    public void ShowWelcome()
    {
        Console.WriteLine("Behavioral Design Patterns Demo in C#");
        Console.WriteLine("Patterns used: Command, Observer, Strategy, Mediator, State");
        ShowStatus();
    }

    private void RunDemoScenario()
    {
        Console.WriteLine();
        Console.WriteLine("Running demo scenario...");

        _invoker.Execute(new ChangeModeCommand(_mediator, new AwayState()));
        _invoker.Execute(new TurnOnCommand(_light, _mediator));
        _invoker.Execute(new ChangeStrategyCommand(_mediator, new ComfortModeStrategy()));
        _invoker.Execute(new ApplyStrategyCommand(_mediator));
        _invoker.Execute(new ChangeModeCommand(_mediator, new NightState()));

        ShowStatus();
    }

    private void ShowStatus()
    {
        Console.WriteLine();
        Console.WriteLine("Device status:");

        foreach (var device in _mediator.Devices)
        {
            Console.WriteLine($"- {device.GetStatus()}");
        }
    }

    private void ShowHistory()
    {
        Console.WriteLine();

        if (_invoker.History.Count == 0)
        {
            Console.WriteLine("No commands executed yet.");
            return;
        }

        Console.WriteLine("Command history:");

        for (var i = 0; i < _invoker.History.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_invoker.History[i]}");
        }
    }
}
