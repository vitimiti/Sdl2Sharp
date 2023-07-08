namespace Sdl2Sharp;

/// <summary>The subsystems used during <see cref="Subsystem" /> initialization.</summary>
[Flags]
public enum SubsystemFlags : uint
{
    /// <summary>No subsystems.</summary>
    None = 0x00000000U,

    /// <summary>The timer subsystem.</summary>
    Timer = 0x00000001U,

    /// <summary>The audio subsystem.</summary>
    /// <remarks>This subsystem implies the <see cref="Events" /> subsystem.</remarks>
    Audio = 0x00000010U,

    /// <summary>The video subsystem.</summary>
    /// <remarks>This subsystem implies the <see cref="Events" /> subsystem.</remarks>
    Video = 0x00000020U,

    /// <summary>The joystick subsystem.</summary>
    /// <remarks>This subsystem implies the <see cref="Events" /> subsystem.</remarks>
    Joystick = 0x00000200U,

    /// <summary>The haptic subsystem.</summary>
    Haptic = 0x00001000U,

    /// <summary>The game controller subsystem.</summary>
    /// <remarks>This subsystem implies the <see cref="Joystick" /> and <see cref="Events" /> subsystems.</remarks>
    GameController = 0x00002000U,

    /// <summary>The events subsystem.</summary>
    Events = 0x00004000U,

    /// <summary>The sensor subsystem.</summary>
    /// <remarks>This subsystem implies the <see cref="Events" /> subsystem.</remarks>
    Sensor = 0x00008000U,

    /// <summary>Don't catch fatal signals.</summary>
    /// <remarks>This flag is obsolete and will be ignored.</remarks>
    [Obsolete("For compatibility, this flag will be ignored", false)]
    NoParachute = 0x00100000U,

    /// <summary>Initialize all subsystems.</summary>
    Everything = Timer | Audio | Video | Events | Joystick | Haptic | GameController | Sensor
}