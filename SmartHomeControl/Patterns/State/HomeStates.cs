namespace SmartHomeControl.Patterns.State;

public interface IHomeAutomationActions
{
    void SetSecurity(bool armed, bool locked);
    void SetLighting(bool enabled, int brightness);
}

public interface IHomeState
{
    string Name { get; }
    void Apply(HomeModeContext context);
}

public sealed class HomeModeContext
{
    public HomeModeContext(IHomeState initialState, IHomeAutomationActions automationActions)
    {
        CurrentState = initialState;
        AutomationActions = automationActions;
    }

    public IHomeState CurrentState { get; private set; }

    public IHomeAutomationActions AutomationActions { get; }

    public void TransitionTo(IHomeState state)
    {
        CurrentState = state;
        state.Apply(this);
    }
}

public sealed class HomeState : IHomeState
{
    public string Name => "Home";

    public void Apply(HomeModeContext context)
    {
        context.AutomationActions.SetSecurity(armed: false, locked: false);
        context.AutomationActions.SetLighting(enabled: true, brightness: 70);
    }
}

public sealed class AwayState : IHomeState
{
    public string Name => "Away";

    public void Apply(HomeModeContext context)
    {
        context.AutomationActions.SetSecurity(armed: true, locked: true);
        context.AutomationActions.SetLighting(enabled: false, brightness: 0);
    }
}

public sealed class NightState : IHomeState
{
    public string Name => "Night";

    public void Apply(HomeModeContext context)
    {
        context.AutomationActions.SetSecurity(armed: true, locked: true);
        context.AutomationActions.SetLighting(enabled: true, brightness: 30);
    }
}
