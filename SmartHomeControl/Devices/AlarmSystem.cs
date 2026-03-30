using SmartHomeControl.Core;

namespace SmartHomeControl.Devices;

public sealed class AlarmSystem : SmartDevice
{
    public AlarmSystem(string name) : base(name)
    {
    }

    public bool IsArmed => IsOn;

    public override void TurnOn()
    {
        if (IsArmed)
        {
            Notify("already armed.");
            return;
        }

        IsOn = true;
        Notify("armed.");
    }

    public override void TurnOff()
    {
        if (!IsArmed)
        {
            Notify("already disarmed.");
            return;
        }

        IsOn = false;
        Notify("disarmed.");
    }

    public override string GetStatus()
    {
        return $"{Name}: {(IsArmed ? "Armed" : "Disarmed")}";
    }
}
