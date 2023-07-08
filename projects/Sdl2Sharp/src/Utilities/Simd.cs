using System.Runtime.InteropServices;

using Sdl2Sharp.Interop;
using Sdl2Sharp.SafeHandles;

namespace Sdl2Sharp.Utilities;

/// <summary>A class to manage SIMD memory.</summary>
public sealed class Simd : IDisposable
{
    /// <summary>Create a SIMD compatible memory block.</summary>
    /// <param name="length">A <see cref="uint" /> with the memory length, in bytes.</param>
    public Simd(uint length)
    {
        Handle = Sdl.SimdAllocate(new CULong(length));
    }

    /// <summary>Get the internal <see cref="SimdSafeHandle" />.</summary>
    public SimdSafeHandle Handle { get; private set; }

    /// <summary>Get a <see cref="uint" /> with the SIMD alignment in bytes.</summary>
    public static uint Alignment => (uint)Sdl.SimdGetAlignment().Value;

    /// <summary>The disposing function.</summary>
    public void Dispose()
    {
        Handle.Dispose();
    }

    /// <summary>Reallocate the SIMD memory block to a new length.</summary>
    /// <param name="length">A <see cref="uint" /> with the memory length, in bytes.</param>
    public void Reallocate(uint length)
    {
        SimdSafeHandle previousHandle = Handle;
        Handle = Sdl.SimdReallocate(previousHandle, new CULong(length));
    }
}