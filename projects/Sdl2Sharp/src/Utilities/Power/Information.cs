using Sdl2Sharp.Interop;

namespace Sdl2Sharp.Utilities.Power;

/// <summary>Class containing power information.</summary>
/// <remarks>
///     <para>
///         Getting power information may be faulty initially and will require multiple checks, making exceptions
///         pointless.
///     </para>
///     <para>
///         Consider checking for <see cref="Power.State.Unknown" /> and values of -1 in the
///         <see cref="Power.BatteryLife" /> members for errors during power information retrieval or the inability to
///         retrieve said information.
///     </para>
/// </remarks>
public static class Information
{
    /// <summary>Get the <see cref="Power.State" /> of the power.</summary>
    public static State State => Sdl.GetPowerInformation(out int _, out int _);

    /// <summary>Get the <see cref="Power.BatteryLife" />.</summary>
    public static BatteryLife BatteryLife
    {
        get
        {
            _ = Sdl.GetPowerInformation(out int seconds, out int percent);
            return new BatteryLife { SecondsLeft = seconds, PercentageLeft = percent };
        }
    }
}