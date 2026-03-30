using SmartHomeControl.Core;
using SmartHomeControl.Patterns.Mediator;
using SmartHomeControl.Patterns.State;
using SmartHomeControl.Patterns.Strategy;

namespace SmartHomeControl.Patterns.Command;

public interface ICommand
{
    string Name { get; }
    void Execute();
}

public sealed class CommandInvoker
{
    private readonly List<string> _history = [];

    public IReadOnlyList<string> History => _history;

    public void Execute(ICommand command)
    {
        command.Execute();
        _history.Add(command.Name);
    }
}

public sealed class TurnOnCommand : ICommand
{
    private readonly SmartDevice _device;
    private readonly ISmartHomeMediator _mediator;

    public TurnOnCommand(SmartDevice device, ISmartHomeMediator mediator)
    {
        _device = device;
        _mediator = mediator;
    }

    public string Name => $"Turn on {_device.Name}";

    public void Execute()
    {
        _device.TurnOn();
        _mediator.HandleDeviceChanged(_device);
    }
}

public sealed class TurnOffCommand : ICommand
{
    private readonly SmartDevice _device;
    private readonly ISmartHomeMediator _mediator;

    public TurnOffCommand(SmartDevice device, ISmartHomeMediator mediator)
    {
        _device = device;
        _mediator = mediator;
    }

    public string Name => $"Turn off {_device.Name}";

    public void Execute()
    {
        _device.TurnOff();
        _mediator.HandleDeviceChanged(_device);
    }
}

public sealed class ChangeModeCommand : ICommand
{
    private readonly ISmartHomeMediator _mediator;
    private readonly IHomeState _state;

    public ChangeModeCommand(ISmartHomeMediator mediator, IHomeState state)
    {
        _mediator = mediator;
        _state = state;
    }

    public string Name => $"Switch mode to {_state.Name}";

    public void Execute()
    {
        _mediator.ChangeMode(_state);
    }
}

public sealed class ChangeStrategyCommand : ICommand
{
    private readonly ISmartHomeMediator _mediator;
    private readonly IAutomationStrategy _strategy;

    public ChangeStrategyCommand(ISmartHomeMediator mediator, IAutomationStrategy strategy)
    {
        _mediator = mediator;
        _strategy = strategy;
    }

    public string Name => $"Switch strategy to {_strategy.Name}";

    public void Execute()
    {
        _mediator.SetStrategy(_strategy);
    }
}

public sealed class ApplyStrategyCommand : ICommand
{
    private readonly ISmartHomeMediator _mediator;

    public ApplyStrategyCommand(ISmartHomeMediator mediator)
    {
        _mediator = mediator;
    }

    public string Name => "Apply active strategy";

    public void Execute()
    {
        _mediator.ApplyStrategy();
    }
}
