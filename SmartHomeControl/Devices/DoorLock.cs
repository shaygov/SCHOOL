using SmartHomeControl.Core;

namespace SmartHomeControl.Devices;

public sealed class DoorLock : SmartDevice
{
    public DoorLock(string name) : base(name)
    {
        IsOn = true;
    }

    public bool IsLocked => IsOn;

    public override void TurnOn()
    {
        if (IsLocked)
        {
            Notify("already locked.");
            return;
        }

        IsOn = true;
        Notify("locked.");
    }

    public override void TurnOff()
    {
        if (!IsLocked)
        {
            Notify("already unlocked.");
            return;
        }

        IsOn = false;
        Notify("unlocked.");
    }

    public override string GetStatus()
    {
        return $"{Name}: {(IsLocked ? "Locked" : "Unlocked")}";
    }
}
