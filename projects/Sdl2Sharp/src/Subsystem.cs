using System.Diagnostics.CodeAnalysis;

using Sdl2Sharp.Exceptions;
using Sdl2Sharp.Interop;

namespace Sdl2Sharp;

/// <summary>The SDL subsystem management.</summary>
/// <remarks>This class inherits from the <see cref="IDisposable" /> interface and should be treated as such.</remarks>
public sealed class Subsystem : IDisposable
{
    /// <summary>Initialize SDL with the given subsystems.</summary>
    /// <param name="flags">The <see cref="SubsystemFlags" /> with the SDL subsystems to initialize.</param>
    /// <exception cref="SubsystemException">
    ///     When SDL is unable to initialize the subsystems marked by the given
    ///     <paramref name="flags" />.
    /// </exception>
    public Subsystem(SubsystemFlags flags)
    {
        int errorCode = Sdl.Initialize(flags);
        if (errorCode < 0)
        {
            throw new SubsystemException(Sdl.GetError(), errorCode);
        }
    }

    /// <summary>Get the initialized subsystems marked in the <see cref="SubsystemFlags" />.</summary>
    public static SubsystemFlags InitializedSubsystems => Sdl.WasInitialized(SubsystemFlags.None);

    /// <summary>The disposing system.</summary>
    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    private static void ReleaseUnmanagedResources()
    {
        Sdl.Quit();
    }

    /// <summary>The <see cref="Subsystem" /> destructor.</summary>
    /// <remarks>Use <see cref="Dispose" /> and the <see langword="using" /> pattern.</remarks>
    ~Subsystem()
    {
        ReleaseUnmanagedResources();
    }

    /// <summary>Initialize SDL subsystems after first initialization.</summary>
    /// <param name="flags">The <see cref="SubsystemFlags" /> with the SDL subsystems to initialize.</param>
    /// <exception cref="SubsystemException">
    ///     When SDL is unable to initialize the subsystems marked by the given
    ///     <paramref name="flags" />.
    /// </exception>
    [SuppressMessage("Performance", "CA1822:Mark members as static",
        Justification = "Avoid accidental omission of subsystem termination")]
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global",
        Justification = "Avoid accidental omission of subsystem termination")]
    public void Start(SubsystemFlags flags)
    {
        int errorCode = Sdl.InitializeSubsystem(flags);
        if (errorCode < 0)
        {
            throw new SubsystemException(Sdl.GetError(), errorCode);
        }
    }

    /// <summary>Stop SDL subsystems after first initialization.</summary>
    /// <param name="flags">The <see cref="SubsystemFlags" /> with the SDL subsystems to terminate.</param>
    /// <remarks>
    ///     <para>Stopping a non initialized subsystem is a no-op.</para>
    ///     <para>Stopping all subsystems is safe, but will leave you uninitialized.</para>
    /// </remarks>
    [SuppressMessage("Performance", "CA1822:Mark members as static",
        Justification = "Avoid accidental omission of subsystem termination")]
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global",
        Justification = "Avoid accidental omission of subsystem termination")]
    public void Stop(SubsystemFlags flags)
    {
        Sdl.QuitSubsystem(flags);
    }
}