using System.Runtime.InteropServices;

namespace Sdl2Sharp.Interop;

internal static partial class Sdl
{
    private const string LibraryName = "SDL2";

    [DllImport(LibraryName, EntryPoint = "SDL_Init", CallingConvention = CallingConvention.Cdecl)]
    public static extern int Initialize(SubsystemFlags flags);

    [DllImport(LibraryName, EntryPoint = "SDL_InitSubSystem", CallingConvention = CallingConvention.Cdecl)]
    public static extern int InitializeSubsystem(SubsystemFlags flags);

    [DllImport(LibraryName, EntryPoint = "SDL_QuitSubSystem", CallingConvention = CallingConvention.Cdecl)]
    public static extern void QuitSubsystem(SubsystemFlags flags);

    [DllImport(LibraryName, EntryPoint = "SDL_WasInit", CallingConvention = CallingConvention.Cdecl)]
    public static extern SubsystemFlags WasInitialized(SubsystemFlags flags);

    [DllImport(LibraryName, EntryPoint = "SDL_Quit", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Quit();
}