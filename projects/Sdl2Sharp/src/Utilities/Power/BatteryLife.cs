namespace Sdl2Sharp.Utilities.Power;

/// <summary>A data structure that holds information about the battery life.</summary>
public struct BatteryLife
{
    /// <summary>An <see cref="int" /> with the battery life left in seconds.</summary>
    /// <remarks>This value will be -1 if the battery life couldn't be determined.</remarks>
    public int SecondsLeft { get; internal init; }

    /// <summary>An <see cref="int" /> with the battery life left in percentage.</summary>
    /// <remarks>
    ///     <para>This value goes from 0 to 100.</para>
    ///     <para>This value will be -1 if the battery life couldn't be determined.</para>
    /// </remarks>
    public int PercentageLeft { get; internal init; }
}