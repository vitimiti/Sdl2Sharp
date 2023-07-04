using System.Runtime.InteropServices;

using Sdl2Sharp.CustomMarshalers;

namespace Sdl2Sharp.Interop;

internal static partial class Sdl
{
    [DllImport(LibraryName, EntryPoint = "SDL_GetError", CallingConvention = CallingConvention.Cdecl,
        CharSet = CharSet.Ansi)]
    [return:
        MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(SdlStringCustomMarshaler),
            MarshalCookie = SdlStringCustomMarshaler.Cookies.LeaveAllocated)]
    public static extern string GetError();
}