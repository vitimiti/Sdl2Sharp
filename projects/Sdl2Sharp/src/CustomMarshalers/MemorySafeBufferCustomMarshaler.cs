using System.Runtime.InteropServices;

using Sdl2Sharp.SafeBuffers;

namespace Sdl2Sharp.CustomMarshalers;

internal sealed class MemorySafeBufferCustomMarshaler : ICustomMarshaler
{
    private static ICustomMarshaler? s_defaultInstance;
    private static ICustomMarshaler? s_allocatedInstance;

    private readonly bool _isAllocated;

    private MemorySafeBufferCustomMarshaler(bool isAllocated)
    {
        _isAllocated = isAllocated;
    }

    public void CleanUpManagedData(object managedObj)
    {
        if (managedObj is not MemorySafeBuffer handle)
        {
            throw new ArgumentException(
                $"{nameof(managedObj)} is expected to be of type {typeof(MemorySafeBuffer)} but was of type {managedObj.GetType()} instead.");
        }

        if (!_isAllocated)
        {
            handle.Dispose();
        }
    }

    public void CleanUpNativeData(IntPtr nativeData)
    {
        if (!_isAllocated)
        {
            Marshal.FreeHGlobal(nativeData);
        }
    }

    public int GetNativeDataSize()
    {
        return Marshal.SizeOf<MemorySafeBuffer>();
    }

    public IntPtr MarshalManagedToNative(object managedObj)
    {
        return managedObj is MemorySafeBuffer memory
            ? Marshal.AllocHGlobal(memory.DangerousGetHandle())
            : throw new ArgumentException(
                $"{nameof(managedObj)} is expected to be of type {typeof(MemorySafeBuffer)} but was of type {managedObj.GetType()} instead.");
    }

    public object MarshalNativeToManaged(IntPtr nativeData)
    {
        return new MemorySafeBuffer(nativeData, true);
    }

    public static ICustomMarshaler GetInstance(string cookie)
    {
        return cookie switch
        {
            Cookies.LeaveAllocated => s_allocatedInstance ??= new MemorySafeBufferCustomMarshaler(true),
            _ => s_defaultInstance ??= new MemorySafeBufferCustomMarshaler(false)
        };
    }
}