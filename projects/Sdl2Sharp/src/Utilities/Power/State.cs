namespace Sdl2Sharp.Utilities.Power;

/// <summary>The basic state for the system's power supply.</summary>
public enum State
{
    /// <summary>Cannot determine power status.</summary>
    Unknown,

    /// <summary>Not plugged in, running on the battery.</summary>
    OnBattery,

    /// <summary>Plugged in, no battery available.</summary>
    NoBattery,

    /// <summary>Plugged in, charging battery.</summary>
    Charging,

    /// <summary>Plugged in, battery charged.</summary>
    Charged
}