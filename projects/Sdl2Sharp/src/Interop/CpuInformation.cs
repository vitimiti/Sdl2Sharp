using System.Runtime.InteropServices;

using Sdl2Sharp.CustomMarshalers;
using Sdl2Sharp.SafeHandles;

namespace Sdl2Sharp.Interop;

internal static partial class Sdl
{
    [DllImport(LibraryName, EntryPoint = "SDL_GetCPUCount", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetCpuCount();

    [DllImport(LibraryName, EntryPoint = "SDL_GetCPUCacheLineSize", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetCpuCacheLineSize();

    [DllImport(LibraryName, EntryPoint = "SDL_HasRDTSC", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool HasRdtsc();

    [DllImport(LibraryName, EntryPoint = "SDL_HasAltiVec", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool HasAltiVec();

    [DllImport(LibraryName, EntryPoint = "SDL_HasMMX", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool HasMmx();

    [DllImport(LibraryName, EntryPoint = "SDL_Has3DNow", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool Has3DNow();

    [DllImport(LibraryName, EntryPoint = "SDL_HasSSE", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool HasSse();

    [DllImport(LibraryName, EntryPoint = "SDL_HasSSE2", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool HasSse2();

    [DllImport(LibraryName, EntryPoint = "SDL_HasSSE3", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool HasSse3();

    [DllImport(LibraryName, EntryPoint = "SDL_HasSSE41", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool HasSse41();

    [DllImport(LibraryName, EntryPoint = "SDL_HasSSE42", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool HasSse42();

    [DllImport(LibraryName, EntryPoint = "SDL_HasAVX", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool HasAvx();

    [DllImport(LibraryName, EntryPoint = "SDL_HasAVX2", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool HasAvx2();

    [DllImport(LibraryName, EntryPoint = "SDL_HasAVX512F", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool HasAvx512F();

    [DllImport(LibraryName, EntryPoint = "SDL_HasARMSIMD", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool HasArmSimd();

    [DllImport(LibraryName, EntryPoint = "SDL_HasNEON", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool HasNeon();

    [DllImport(LibraryName, EntryPoint = "SDL_GetSystemRAM", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetSystemRam();

    [DllImport(LibraryName, EntryPoint = "SDL_SIMDGetAlignment", CallingConvention = CallingConvention.Cdecl)]
    public static extern CULong SimdGetAlignment();

    [DllImport(LibraryName, EntryPoint = "SDL_SIMDAlloc", CallingConvention = CallingConvention.Cdecl)]
    [return:
        MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(SimdCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
    public static extern SimdSafeHandle SimdAllocate(CULong length);

    [DllImport(LibraryName, EntryPoint = "SDL_SIMDRealloc", CallingConvention = CallingConvention.Cdecl)]
    [return:
        MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(SimdCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
    public static extern SimdSafeHandle SimdReallocate(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(SimdCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        SimdSafeHandle memory, CULong length);

    [DllImport(LibraryName, EntryPoint = "SDL_SIMDFree", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SimdFree(IntPtr memory);
}