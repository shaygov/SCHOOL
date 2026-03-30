using SmartHomeControl.Core;

namespace SmartHomeControl.Devices;

public sealed class Light : SmartDevice
{
    public Light(string name) : base(name)
    {
        Brightness = 50;
    }

    public int Brightness { get; private set; }

    public void SetBrightness(int brightness)
    {
        Brightness = Math.Clamp(brightness, 0, 100);
        Notify($"brightness set to {Brightness}%.");
    }

    public override string GetStatus()
    {
        var state = IsOn ? "On" : "Off";
        return $"{Name}: {state}, brightness {Brightness}%";
    }
}
