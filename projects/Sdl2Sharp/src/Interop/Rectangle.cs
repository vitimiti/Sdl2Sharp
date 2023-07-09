using System.Runtime.InteropServices;

namespace Sdl2Sharp.Interop;

internal static partial class Sdl
{
    [DllImport(LibraryName, EntryPoint = "SDL_EnclosePoints", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool EnclosePoints(
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 1)]
        NativePoint[] points, int count, in NativeRectangle clip, out NativeRectangle result);

    [DllImport(LibraryName, EntryPoint = "SDL_EnclosePoints", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool EnclosePoints(
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 1)]
        NativePoint[] points, int count, IntPtr clip, out NativeRectangle result);

    [DllImport(LibraryName, EntryPoint = "SDL_IntersectRectAndLine", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool IntersectRectangleAndLine(in NativeRectangle rectangle, ref int x1, ref int y1,
        ref int x2, ref int y2);

    [StructLayout(LayoutKind.Sequential)]
    public struct NativePoint
    {
        public int X;
        public int Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NativeRectangle
    {
        public int X;
        public int Y;
        public int W;
        public int H;
    }
}