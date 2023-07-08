using System.Runtime.InteropServices;

using Sdl2Sharp.CustomMarshalers;

namespace Sdl2Sharp.Interop;

internal static partial class Sdl
{
    [DllImport(LibraryName, EntryPoint = "SDL_GetVersion", CallingConvention = CallingConvention.Cdecl)]
    public static extern void GetVersion(out NativeVersion nativeVersion);

    [DllImport(LibraryName, EntryPoint = "SDL_GetRevision", CallingConvention = CallingConvention.Cdecl)]
    [return:
        MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(SdlStringCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
    public static extern string GetRevision();

    [StructLayout(LayoutKind.Sequential)]
    public struct NativeVersion
    {
        public readonly byte Major;
        public readonly byte Minor;
        public readonly byte Patch;
    }
}