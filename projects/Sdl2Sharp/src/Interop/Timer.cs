using System.Runtime.InteropServices;

using Sdl2Sharp.CustomMarshalers;

using Timer = Sdl2Sharp.Utilities.Timer;

namespace Sdl2Sharp.Interop;

internal static partial class Sdl
{
    [DllImport(LibraryName, EntryPoint = "SDL_GetTicks", CallingConvention = CallingConvention.Cdecl)]
    public static extern uint GetTicks();

    [DllImport(LibraryName, EntryPoint = "SDL_GetTicks64", CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong GetTicks64();

    [DllImport(LibraryName, EntryPoint = "SDL_GetPerformanceCounter", CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong GetPerformanceCounter();

    [DllImport(LibraryName, EntryPoint = "SDL_GetPerformanceFrequency", CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong GetPerformanceFrequency();

    [DllImport(LibraryName, EntryPoint = "SDL_Delay", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Delay(uint milliseconds);

    [DllImport(LibraryName, EntryPoint = "SDL_AddTimer", CallingConvention = CallingConvention.Cdecl)]
    public static extern int AddTimer(uint interval, Timer.Callback callback,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VoidPointerCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        object parameter);

    [DllImport(LibraryName, EntryPoint = "SDL_RemoveTimer", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool RemoveTimer(int id);
}