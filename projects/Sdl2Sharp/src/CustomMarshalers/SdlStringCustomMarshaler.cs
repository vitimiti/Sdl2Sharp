using System.Runtime.InteropServices;

namespace Sdl2Sharp.CustomMarshalers;

internal sealed class SdlStringCustomMarshaler : ICustomMarshaler
{
    private static ICustomMarshaler? s_defaultInstance;
    private static ICustomMarshaler? s_allocatedInstance;

    private readonly bool _isAllocated;

    private SdlStringCustomMarshaler(bool isAllocated)
    {
        _isAllocated = isAllocated;
    }

    public void CleanUpManagedData(object managedObj)
    {
    }

    public void CleanUpNativeData(IntPtr nativeData)
    {
        if (!_isAllocated)
        {
            Marshal.FreeCoTaskMem(nativeData);
        }
    }

    public int GetNativeDataSize()
    {
        return Marshal.SizeOf<string>();
    }

    public IntPtr MarshalManagedToNative(object managedObj)
    {
        return managedObj is string str
            ? Marshal.StringToCoTaskMemUTF8(str)
            : throw new ArgumentException(
                $"{nameof(managedObj)} is expected to be of type {typeof(string)} but was of type {managedObj.GetType()} instead.");
    }

    public object MarshalNativeToManaged(IntPtr nativeData)
    {
        return Marshal.PtrToStringUTF8(nativeData) ?? string.Empty;
    }

    public static ICustomMarshaler GetInstance(string cookie)
    {
        return cookie switch
        {
            Cookies.LeaveAllocated => s_allocatedInstance ??= new SdlStringCustomMarshaler(true),
            _ => s_defaultInstance ??= new SdlStringCustomMarshaler(false)
        };
    }

    public struct Cookies
    {
        public const string LeaveAllocated = "LeaveAllocatedCookie";
    }
}