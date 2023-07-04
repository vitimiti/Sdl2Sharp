using Sdl2Sharp.Interop;

namespace Sdl2Sharp.Utilities;

/// <summary>Retrieve information from the native SDL2 DLL.</summary>
public static class NativeDllInformation
{
    /// <summary>Get the native SDL2 DLL <see cref="System.Version" /> information.</summary>
    /// <remarks>
    ///     <para>
    ///         Since SDL2 version 2.0.16, this <see cref="System.Version" /> uses the constructor
    ///         <see cref="System.Version(int, int, int)" /> instead of the constructor
    ///         <see cref="System.Version(int, int, int, int)" /> as the function SDL_GetRevisionNumber() becomes obsolete.
    ///     </para>
    ///     <para>Now the only way to get a revision is through <see cref="Revision" />.</para>
    /// </remarks>
    public static Version Version
    {
        get
        {
            Sdl.GetVersion(out Sdl.NativeVersion nativeVersion);
            return new Version(nativeVersion.Major, nativeVersion.Minor, nativeVersion.Patch);
        }
    }

    /// <summary>Get a <see cref="string" /> with the native SDL2 DLL revision hash value.</summary>
    public static string Revision => Sdl.GetRevision();
}