using Microsoft.Win32.SafeHandles;

using Sdl2Sharp.Interop;

namespace Sdl2Sharp.SafeHandles;

/// <summary>A handle to safely represent read/write operations.</summary>
/// <remarks>
///     This class inherits from the <see cref="SafeHandleZeroOrMinusOneIsInvalid" /> class and should be treated as
///     such.
/// </remarks>
public sealed class ReadWriteOperationsSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    internal ReadWriteOperationsSafeHandle(IntPtr ptr, bool ownsHandle) : base(ownsHandle)
    {
        handle = ptr;
    }

    /// <summary>The method to release the internal handle.</summary>
    /// <returns><see langword="true" /> when the handle was successfully released.</returns>
    protected override bool ReleaseHandle()
    {
        if (handle == IntPtr.Zero || IsClosed || IsInvalid)
        {
            return handle == IntPtr.Zero;
        }

        Sdl.FreeReadWrite(handle);
        handle = IntPtr.Zero;

        return handle == IntPtr.Zero;
    }
}