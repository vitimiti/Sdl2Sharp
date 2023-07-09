using System.Runtime.InteropServices;

namespace Sdl2Sharp.SafeBuffers;

/// <summary>A handle to safely represent memory buffers.</summary>
/// <remarks>This class inherits from the <see cref="SafeBuffer" /> class and should be treated as such.</remarks>
public class MemorySafeBuffer : SafeBuffer
{
    internal MemorySafeBuffer(IntPtr ptr, bool ownsHandle) : base(ownsHandle)
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

        Marshal.FreeHGlobal(handle);
        handle = IntPtr.Zero;

        return handle == IntPtr.Zero;
    }
}