using System.Runtime.InteropServices;

using Sdl2Sharp.Utilities.Power;

namespace Sdl2Sharp.Interop;

internal static partial class Sdl
{
    [DllImport(LibraryName, EntryPoint = "SDL_GetPowerInfo", CallingConvention = CallingConvention.Cdecl)]
    public static extern State GetPowerInformation(out int seconds, out int percent);
}