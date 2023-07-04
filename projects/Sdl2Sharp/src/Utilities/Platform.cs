using Sdl2Sharp.Interop;

namespace Sdl2Sharp.Utilities;

/// <summary>Get platform related information.</summary>
public static class Platform
{
    /// <summary>Get a <see cref="string" /> with the platform name.</summary>
    public static string Name => Sdl.GetPlatform();
}