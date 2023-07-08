using System.Runtime.InteropServices;

using Sdl2Sharp.CustomMarshalers;

namespace Sdl2Sharp.Interop;

internal static partial class Sdl
{
    [DllImport(LibraryName, EntryPoint = "SDL_SetClipboardText", CallingConvention = CallingConvention.Cdecl,
        CharSet = CharSet.Ansi)]
    public static extern int SetClipboardText(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(SdlStringCustomMarshaler))]
        string text);

    [DllImport(LibraryName, EntryPoint = "SDL_GetClipboardText", CallingConvention = CallingConvention.Cdecl,
        CharSet = CharSet.Ansi)]
    [return:
        MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(SdlStringCustomMarshaler),
            MarshalCookie = SdlStringCustomMarshaler.Cookies.LeaveAllocated)]
    public static extern string GetClipboardText();

    [DllImport(LibraryName, EntryPoint = "SDL_HasClipboardText", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool HasClipboardText();
}