using System.Runtime.InteropServices;

using Sdl2Sharp.CustomMarshalers;
using Sdl2Sharp.Utilities.Log;

namespace Sdl2Sharp.Interop;

internal static partial class Sdl
{
    [DllImport(LibraryName, EntryPoint = "SDL_LogSetAllPriority", CallingConvention = CallingConvention.Cdecl)]
    public static extern void LogSetAllPriority(Priority priority);

    [DllImport(LibraryName, EntryPoint = "SDL_LogSetPriority", CallingConvention = CallingConvention.Cdecl)]
    public static extern void LogSetPriority(int category, Priority priority);

    [DllImport(LibraryName, EntryPoint = "SDL_LogGetPriority", CallingConvention = CallingConvention.Cdecl)]
    public static extern Priority LogGetPriority(int category);

    [DllImport(LibraryName, EntryPoint = "SDL_LogResetPriorities", CallingConvention = CallingConvention.Cdecl)]
    public static extern void LogResetPriorities();

    [DllImport(LibraryName, EntryPoint = "SDL_Log", CallingConvention = CallingConvention.Cdecl,
        CharSet = CharSet.Ansi)]
    public static extern void Log(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(SdlStringCustomMarshaler))]
        string message);

    [DllImport(LibraryName, EntryPoint = "SDL_LogVerbose", CallingConvention = CallingConvention.Cdecl,
        CharSet = CharSet.Ansi)]
    public static extern void LogVerbose(int category,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(SdlStringCustomMarshaler))]
        string message);

    [DllImport(LibraryName, EntryPoint = "SDL_LogDebug", CallingConvention = CallingConvention.Cdecl,
        CharSet = CharSet.Ansi)]
    public static extern void LogDebug(int category,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(SdlStringCustomMarshaler))]
        string message);

    [DllImport(LibraryName, EntryPoint = "SDL_LogInfo", CallingConvention = CallingConvention.Cdecl,
        CharSet = CharSet.Ansi)]
    public static extern void LogInformation(int category,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(SdlStringCustomMarshaler))]
        string message);

    [DllImport(LibraryName, EntryPoint = "SDL_LogWarn", CallingConvention = CallingConvention.Cdecl,
        CharSet = CharSet.Ansi)]
    public static extern void LogWarning(int category,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(SdlStringCustomMarshaler))]
        string message);

    [DllImport(LibraryName, EntryPoint = "SDL_LogError", CallingConvention = CallingConvention.Cdecl,
        CharSet = CharSet.Ansi)]
    public static extern void LogError(int category,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(SdlStringCustomMarshaler))]
        string message);

    [DllImport(LibraryName, EntryPoint = "SDL_LogCritical", CallingConvention = CallingConvention.Cdecl,
        CharSet = CharSet.Ansi)]
    public static extern void LogCritical(int category,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(SdlStringCustomMarshaler))]
        string message);

    [DllImport(LibraryName, EntryPoint = "SDL_LogMessage", CallingConvention = CallingConvention.Cdecl,
        CharSet = CharSet.Ansi)]
    public static extern void LogMessage(int category, Priority priority,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(SdlStringCustomMarshaler))]
        string message);

    [DllImport(LibraryName, EntryPoint = "SDL_LogGetOutputFunction")]
    public static extern void LogGetOutputFunction(out Functions.LogOutputFunction callback,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VoidPointerCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        [Out]
        out object userData);

    [DllImport(LibraryName, EntryPoint = "SDL_LogSetOutputFunction")]
    public static extern void LogSetOutputFunction(Functions.LogOutputFunction callback,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VoidPointerCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        object userData);
}