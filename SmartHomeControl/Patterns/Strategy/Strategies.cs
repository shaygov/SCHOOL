using SmartHomeControl.Devices;

namespace SmartHomeControl.Patterns.Strategy;

public interface IAutomationStrategy
{
    string Name { get; }
    void Apply(AirConditioner airConditioner, Light light);
}

public sealed class EcoModeStrategy : IAutomationStrategy
{
    public string Name => "Eco";

    public void Apply(AirConditioner airConditioner, Light light)
    {
        airConditioner.TurnOn();
        airConditioner.SetTargetTemperature(24);
        light.TurnOn();
        light.SetBrightness(40);
    }
}

public sealed class ComfortModeStrategy : IAutomationStrategy
{
    public string Name => "Comfort";

    public void Apply(AirConditioner airConditioner, Light light)
    {
        airConditioner.TurnOn();
        airConditioner.SetTargetTemperature(21);
        light.TurnOn();
        light.SetBrightness(80);
    }
}
