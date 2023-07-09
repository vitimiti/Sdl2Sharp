using System.Runtime.InteropServices;

using Sdl2Sharp.CustomMarshalers;
using Sdl2Sharp.SafeBuffers;
using Sdl2Sharp.SafeHandles;

namespace Sdl2Sharp.Interop;

internal static partial class Sdl
{
    [DllImport(LibraryName, EntryPoint = "SDL_RWFromFile", CallingConvention = CallingConvention.Cdecl,
        CharSet = CharSet.Ansi)]
    [return:
        MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
    public static extern ReadWriteOperationsSafeHandle ReadWriteFromFile(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(SdlStringCustomMarshaler))]
        string file,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(SdlStringCustomMarshaler))]
        string mode);

    [DllImport(LibraryName, EntryPoint = "SDL_RWFromFP", CallingConvention = CallingConvention.Cdecl)]
    [return:
        MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
    public static extern ReadWriteOperationsSafeHandle ReadWriteFromFilePointer(IntPtr filePointer,
        [MarshalAs(UnmanagedType.I1)] bool autoClose);

    [DllImport(LibraryName, EntryPoint = "SDL_RWFromMem", CallingConvention = CallingConvention.Cdecl)]
    [return:
        MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
    public static extern ReadWriteOperationsSafeHandle ReadWriteFromMemory(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MemorySafeBufferCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        ref MemorySafeBuffer memory, int size);

    [DllImport(LibraryName, EntryPoint = "SDL_RWFromConstMem", CallingConvention = CallingConvention.Cdecl)]
    [return:
        MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
    public static extern ReadWriteOperationsSafeHandle ReadWriteFromConstMemory(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MemorySafeBufferCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        MemorySafeBuffer memory, int size);

    [DllImport(LibraryName, EntryPoint = "SDL_AllocRW", CallingConvention = CallingConvention.Cdecl)]
    [return:
        MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
    public static extern ReadWriteOperationsSafeHandle AllocateReadWrite();

    [DllImport(LibraryName, EntryPoint = "SDL_FreeRW", CallingConvention = CallingConvention.Cdecl)]
    public static extern void FreeReadWrite(IntPtr area);

    [DllImport(LibraryName, EntryPoint = "SDL_RWsize", CallingConvention = CallingConvention.Cdecl)]
    public static extern long ReadWriteSize(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        ref ReadWriteOperationsSafeHandle context);

    [DllImport(LibraryName, EntryPoint = "SDL_RWseek", CallingConvention = CallingConvention.Cdecl)]
    public static extern long ReadWriteSeek(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        ref ReadWriteOperationsSafeHandle context, long offset, SeekOrigin whence);

    [DllImport(LibraryName, EntryPoint = "SDL_RWtell", CallingConvention = CallingConvention.Cdecl)]
    public static extern long ReadWriteTell(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        ref ReadWriteOperationsSafeHandle context);

    [DllImport(LibraryName, EntryPoint = "SDL_RWread", CallingConvention = CallingConvention.Cdecl)]
    public static extern CULong ReadWriteRead(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        ref ReadWriteOperationsSafeHandle context,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MemorySafeBufferCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        ref MemorySafeBuffer ptr, CULong size, CULong maximumNumber);

    [DllImport(LibraryName, EntryPoint = "SDL_RWwrite", CallingConvention = CallingConvention.Cdecl)]
    public static extern CULong ReadWriteWrite(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        ref ReadWriteOperationsSafeHandle context,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MemorySafeBufferCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        MemorySafeBuffer ptr, CULong size, CULong number);

    [DllImport(LibraryName, EntryPoint = "SDL_RWclose", CallingConvention = CallingConvention.Cdecl)]
    public static extern int ReadWriteClose(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        ref ReadWriteOperationsSafeHandle context);

    [DllImport(LibraryName, EntryPoint = "SDL_LoadFile_RW", CallingConvention = CallingConvention.Cdecl)]
    [return:
        MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MemorySafeBufferCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
    public static extern MemorySafeBuffer LoadFileReadWrite(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        ref ReadWriteOperationsSafeHandle src, out CULong size, [MarshalAs(UnmanagedType.I1)] bool freeSrc);

    [DllImport(LibraryName, EntryPoint = "SDL_LoadFile_RW", CallingConvention = CallingConvention.Cdecl)]
    [return:
        MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MemorySafeBufferCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
    public static extern MemorySafeBuffer LoadFileReadWrite(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        ref ReadWriteOperationsSafeHandle src, IntPtr size, [MarshalAs(UnmanagedType.I1)] bool freeSrc);

    [DllImport(LibraryName, EntryPoint = "SDL_LoadFile", CallingConvention = CallingConvention.Cdecl)]
    [return:
        MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MemorySafeBufferCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
    public static extern MemorySafeBuffer LoadFile(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(SdlStringCustomMarshaler))]
        string file,
        out CULong size);

    [DllImport(LibraryName, EntryPoint = "SDL_LoadFile", CallingConvention = CallingConvention.Cdecl)]
    [return:
        MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MemorySafeBufferCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
    public static extern MemorySafeBuffer LoadFile(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(SdlStringCustomMarshaler))]
        string file,
        IntPtr size);

    [DllImport(LibraryName, EntryPoint = "SDL_ReadU8", CallingConvention = CallingConvention.Cdecl)]
    public static extern byte ReadUnsigned8(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        ref ReadWriteOperationsSafeHandle src);

    [DllImport(LibraryName, EntryPoint = "SDL_ReadLE16", CallingConvention = CallingConvention.Cdecl)]
    public static extern ushort ReadLittleEndian16(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        ref ReadWriteOperationsSafeHandle src);

    [DllImport(LibraryName, EntryPoint = "SDL_ReadBE16", CallingConvention = CallingConvention.Cdecl)]
    public static extern ushort ReadBigEndian16(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        ref ReadWriteOperationsSafeHandle src);

    [DllImport(LibraryName, EntryPoint = "SDL_ReadLE32", CallingConvention = CallingConvention.Cdecl)]
    public static extern uint ReadLittleEndian32(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        ref ReadWriteOperationsSafeHandle src);

    [DllImport(LibraryName, EntryPoint = "SDL_ReadBE32", CallingConvention = CallingConvention.Cdecl)]
    public static extern uint ReadBigEndian32(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        ref ReadWriteOperationsSafeHandle src);

    [DllImport(LibraryName, EntryPoint = "SDL_ReadLE64", CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong ReadLittleEndian64(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        ref ReadWriteOperationsSafeHandle src);

    [DllImport(LibraryName, EntryPoint = "SDL_ReadBE64", CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong ReadBigEndian64(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        ref ReadWriteOperationsSafeHandle src);

    [DllImport(LibraryName, EntryPoint = "SDL_WriteU8", CallingConvention = CallingConvention.Cdecl)]
    public static extern CULong WriteUnsigned8(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        ref ReadWriteOperationsSafeHandle src, byte value);

    [DllImport(LibraryName, EntryPoint = "SDL_WriteLE16", CallingConvention = CallingConvention.Cdecl)]
    public static extern CULong WriteLittleEndian16(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        ref ReadWriteOperationsSafeHandle src, ushort value);

    [DllImport(LibraryName, EntryPoint = "SDL_WriteBE16", CallingConvention = CallingConvention.Cdecl)]
    public static extern CULong WriteBigEndian16(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        ref ReadWriteOperationsSafeHandle src, ushort value);

    [DllImport(LibraryName, EntryPoint = "SDL_WriteLE32", CallingConvention = CallingConvention.Cdecl)]
    public static extern CULong WriteLittleEndian32(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        ref ReadWriteOperationsSafeHandle src, uint value);

    [DllImport(LibraryName, EntryPoint = "SDL_WriteBE32", CallingConvention = CallingConvention.Cdecl)]
    public static extern CULong WriteBigEndian32(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        ref ReadWriteOperationsSafeHandle src, uint value);

    [DllImport(LibraryName, EntryPoint = "SDL_WriteLE64", CallingConvention = CallingConvention.Cdecl)]
    public static extern CULong WriteLittleEndian64(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        ref ReadWriteOperationsSafeHandle src, ulong value);

    [DllImport(LibraryName, EntryPoint = "SDL_WriteBE64", CallingConvention = CallingConvention.Cdecl)]
    public static extern CULong WriteBigEndian64(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        ref ReadWriteOperationsSafeHandle src, ulong value);
}