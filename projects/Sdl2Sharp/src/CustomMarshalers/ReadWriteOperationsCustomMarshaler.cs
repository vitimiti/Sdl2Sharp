using System.Runtime.InteropServices;

using Sdl2Sharp.Interop;
using Sdl2Sharp.SafeHandles;

namespace Sdl2Sharp.CustomMarshalers;

internal class ReadWriteOperationsCustomMarshaler : ICustomMarshaler
{
    private static ICustomMarshaler? s_defaultInstance;
    private static ICustomMarshaler? s_allocatedInstance;

    private readonly bool _isAllocated;

    private ReadWriteOperationsCustomMarshaler(bool isAllocated)
    {
        _isAllocated = isAllocated;
    }

    public void CleanUpManagedData(object managedObj)
    {
        if (managedObj is not ReadWriteOperationsSafeHandle handle)
        {
            throw new ArgumentException(
                $"{nameof(managedObj)} is expected to be of type {typeof(ReadWriteOperationsSafeHandle)} but was of type {managedObj.GetType()} instead.");
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
            Sdl.FreeReadWrite(nativeData);
        }
    }

    public int GetNativeDataSize()
    {
        return Marshal.SizeOf<ReadWriteOperationsSafeHandle>();
    }

    public IntPtr MarshalManagedToNative(object managedObj)
    {
        return managedObj is ReadWriteOperationsSafeHandle handle
            ? handle.DangerousGetHandle()
            : throw new ArgumentException(
                $"{nameof(managedObj)} is expected to be of type {typeof(ReadWriteOperationsSafeHandle)} but was of type {managedObj.GetType()} instead.");
    }

    public object MarshalNativeToManaged(IntPtr nativeData)
    {
        return new ReadWriteOperationsSafeHandle(nativeData, true);
    }

    public static ICustomMarshaler GetInstance(string cookie)
    {
        return cookie switch
        {
            Cookies.LeaveAllocated => s_allocatedInstance ??= new ReadWriteOperationsCustomMarshaler(true),
            _ => s_defaultInstance ??= new ReadWriteOperationsCustomMarshaler(false)
        };
    }
}