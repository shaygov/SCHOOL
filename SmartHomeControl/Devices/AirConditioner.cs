using SmartHomeControl.Core;

namespace SmartHomeControl.Devices;

public sealed class AirConditioner : SmartDevice
{
    public AirConditioner(string name) : base(name)
    {
        TargetTemperature = 22;
    }

    public int TargetTemperature { get; private set; }

    public void SetTargetTemperature(int temperature)
    {
        TargetTemperature = Math.Clamp(temperature, 16, 30);
        Notify($"target temperature set to {TargetTemperature}C.");
    }

    public override string GetStatus()
    {
        var state = IsOn ? "On" : "Off";
        return $"{Name}: {state}, target {TargetTemperature}C";
    }
}
