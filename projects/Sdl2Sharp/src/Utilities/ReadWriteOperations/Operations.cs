using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using Sdl2Sharp.CustomMarshalers;
using Sdl2Sharp.Exceptions;
using Sdl2Sharp.Interop;
using Sdl2Sharp.SafeBuffers;
using Sdl2Sharp.SafeHandles;

namespace Sdl2Sharp.Utilities.ReadWriteOperations;

/// <summary>A class to manage read/write operations and streams.</summary>
public sealed class Operations
{
    /// <summary>Callback to close and free an allocated <see cref="ReadWriteOperationsSafeHandle" />.</summary>
    /// <param name="context">
    ///     A <see langword="ref" /> parameter to the <see cref="ReadWriteOperationsSafeHandle" /> to close
    ///     and free.
    /// </param>
    /// <returns>An <see cref="int" /> with a value of 1 on success and a value of 0 on error or end of file.</returns>
    /// <remarks>You can use <see cref="Error.DangerousGetLast()" /> to get error information.</remarks>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int CloseCallback(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        ref ReadWriteOperationsSafeHandle context);

    /// <summary>
    ///     Callback to read up to <paramref name="maximumNumber" /> of objects each of size `<paramref name="size" />` from
    ///     the data stream to the object pointed at by <paramref name="ptr" />.
    /// </summary>
    /// <param name="context">
    ///     A <see langword="ref" /> parameter to the <see cref="ReadWriteOperationsSafeHandle" /> to read
    ///     from.
    /// </param>
    /// <param name="ptr">An <see langword="out" /> parameter to the <see cref="MemorySafeBuffer" /> to read into.</param>
    /// <param name="size">A <see cref="CULong" /> with the size of each object.</param>
    /// <param name="maximumNumber">A <see cref="CULong" /> with the maximum number of objects to read.</param>
    /// <returns>A <see cref="CULong" /> with the number of objects read or 0 at error or end of file.</returns>
    /// <remarks>You can use <see cref="Error.DangerousGetLast()" /> to get error information.</remarks>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate CULong ReadCallback(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        ref ReadWriteOperationsSafeHandle context,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MemorySafeBufferCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        out MemorySafeBuffer ptr, CULong size, CULong maximumNumber);

    /// <summary>Callback to seek to <paramref name="offset" /> relative to <paramref name="whence" />.</summary>
    /// <param name="context">
    ///     A <see langword="ref" /> parameter to the <see cref="ReadWriteOperationsSafeHandle" /> to seek
    ///     in.
    /// </param>
    /// <param name="offset">A <see cref="long" /> with the offset to seek to.</param>
    /// <param name="whence">A <see cref="SeekOrigin" /> to seek from.</param>
    /// <returns>A <see cref="long" /> with the final <paramref name="offset" /> in the data stream or -1 on error.</returns>
    /// <remarks>You can use <see cref="Error.DangerousGetLast()" /> to get error information.</remarks>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate long SeekCallback(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        ref ReadWriteOperationsSafeHandle context, long offset, SeekOrigin whence);

    /// <summary>Return the size of the file stream.</summary>
    /// <param name="context">
    ///     A <see langword="ref" /> parameter to the <see cref="ReadWriteOperationsSafeHandle" /> stream to get the
    ///     size from.
    /// </param>
    /// <returns>A <see cref="long" /> with the size of the <paramref name="context" /> or -1 if unknown.</returns>
    /// <remarks>You can use <see cref="Error.DangerousGetLast()" /> to get error information.</remarks>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate long SizeCallback(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        ref ReadWriteOperationsSafeHandle context);

    /// <summary>
    ///     Write exactly <paramref name="number" /> of objects of size <paramref name="size" /> from the area object
    ///     <paramref name="ptr" /> to the data stream.
    /// </summary>
    /// <param name="context">
    ///     A <see langword="ref" /> parameter to the <see cref="ReadWriteOperationsSafeHandle" /> to write
    ///     into.
    /// </param>
    /// <param name="ptr">The <see cref="object" /> to write into the <paramref name="context" />.</param>
    /// <param name="size">A <see cref="CULong" /> with the size of the objects to write.</param>
    /// <param name="number">A <see cref="CULong" /> with the number of objects to write.</param>
    /// <returns>A <see cref="CULong" /> with the number of files written or 0 at error or end of file.</returns>
    /// <remarks>You can use <see cref="Error.DangerousGetLast()" /> to get error information.</remarks>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate CULong WriteCallback(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ReadWriteOperationsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        ref ReadWriteOperationsSafeHandle context,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MemorySafeBufferCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        MemorySafeBuffer ptr, CULong size, CULong number);

    /// <summary>Create a read/write operations class for reading from and/or writing to a named file.</summary>
    /// <param name="file">A <see cref="string" /> with the file to open.</param>
    /// <param name="access">A <see cref="FileAccess" /> with the access permissions to the <paramref name="file" />.</param>
    /// <param name="mode">A <see cref="FileMode" /> with the mode permissions to the <paramref name="file" />.</param>
    /// <param name="isBinary">Whether the <paramref name="file" /> is binary or not.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     When the combination of <paramref name="access" /> and
    ///     <paramref name="mode" /> is invalid.
    /// </exception>
    /// <exception cref="ReadWriteOperationsException">When SDL is unable to create the internal handle.</exception>
    /// <remarks>
    ///     <para>
    ///         The available combinations for the <paramref name="access" /> and <paramref name="mode" /> are limited and the
    ///         only
    ///         valid options are:
    ///         <code>
    /// var valid1 = new Operations("filePath", FileAccess.Read, FileMode.Open", false);              // Equivalent to "r"
    /// var valid2 = new Operations("filePath", FileAccess.Write, FileMode.OpenOrCreate", false);     // Equivalent to "w"
    /// var valid3 = new Operations("filePath", FileAccess.Write, FileMode.Append", false);           // Equivalent to "a"
    /// var valid4 = new Operations("filePath", FileAccess.ReadWrite, FileMode.Open", false);         // Equivalent to "r+"
    /// var valid5 = new Operations("filePath", FileAccess.ReadWrite, FileMode.OpenOrCreate", false); // Equivalent to "w+"
    /// var valid6 = new Operations("filePath", FileAccess.ReadWrite, FileMode.Append", false);       // Equivalent to "a+"
    /// var valid7 = new Operations("filePath", FileAccess.Read, FileMode.Open", true);               // Equivalent to "rb"
    /// var valid8 = new Operations("filePath", FileAccess.Write, FileMode.OpenOrCreate", true);      // Equivalent to "wb"
    /// var valid9 = new Operations("filePath", FileAccess.Write, FileMode.Append", true);            // Equivalent to "ab"
    /// var valid10 = new Operations("filePath", FileAccess.ReadWrite, FileMode.Open", true);         // Equivalent to "r+b"
    /// var valid11 = new Operations("filePath", FileAccess.ReadWrite, FileMode.OpenOrCreate", true); // Equivalent to "w+b"
    /// var valid12 = new Operations("filePath", FileAccess.ReadWrite, FileMode.Append", true);       // Equivalent to "a+b"
    ///         </code>
    ///     </para>
    ///     <para>
    ///         This function supports Unicode filenames, but they must be encoded in UTF-8 format, regardless of the
    ///         underlying operating system.
    ///     </para>
    ///     <para>As a fallback, this will transparently open a matching filename in an Android app's `assets`.</para>
    ///     <para>Closing the operations will close the file handle SDL is holding internally.</para>
    /// </remarks>
    public Operations(string file, FileAccess access, FileMode mode, bool isBinary)
    {
        string modeStr = access switch
        {
            FileAccess.Read when mode is FileMode.Open => "r",
            FileAccess.Write when mode is FileMode.OpenOrCreate => "w",
            FileAccess.Write when mode is FileMode.Append => "a",
            FileAccess.ReadWrite when mode is FileMode.Open => "r+",
            FileAccess.ReadWrite when mode is FileMode.OpenOrCreate => "w+",
            FileAccess.ReadWrite when mode is FileMode.Append => "a+",
            _ => throw new ArgumentOutOfRangeException(nameof(access), access, null)
        };

        if (isBinary)
        {
            modeStr += "b";
        }

        Handle = Sdl.ReadWriteFromFile(file, modeStr);
        if (Handle.IsClosed || Handle.IsInvalid)
        {
            throw new ReadWriteOperationsException(Sdl.GetError());
        }
    }

    /// <summary>Create a read/write operations from a file stream.</summary>
    /// <param name="fileStream">The <see cref="FileStream" /> to open.</param>
    /// <param name="autoClose">Whether to automatically close the <paramref name="fileStream" /> or not.</param>
    /// <exception cref="ReadWriteOperationsException">
    ///     When SDL is unable to create a read/write operations from the given
    ///     <paramref name="fileStream" />.
    /// </exception>
    public Operations(FileStream fileStream, bool autoClose)
    {
        Handle = Sdl.ReadWriteFromFilePointer(fileStream.SafeFileHandle.DangerousGetHandle(), autoClose);
        if (Handle.IsClosed || Handle.IsInvalid)
        {
            throw new ReadWriteOperationsException(Sdl.GetError());
        }
    }

    /// <summary>Create a read/write operations from a memory buffer.</summary>
    /// <param name="memory">A <see langword="ref" /> parameter to a <see cref="MemorySafeBuffer" /> to read/write.</param>
    /// <exception cref="ReadWriteOperationsException">
    ///     When SDL is unable to create a read/write operations from the given
    ///     <paramref name="memory" />.
    /// </exception>
    public Operations(ref MemorySafeBuffer memory)
    {
        Handle = Sdl.ReadWriteFromMemory(ref memory, (int)memory.ByteLength);
        if (Handle.IsClosed || Handle.IsInvalid)
        {
            throw new ReadWriteOperationsException(Sdl.GetError());
        }
    }

    /// <summary>Create a read/write operations from a memory buffer.</summary>
    /// <param name="memory">A <see cref="MemorySafeBuffer" /> to read.</param>
    /// <exception cref="ReadWriteOperationsException">
    ///     When SDL is unable to create a read/write operations from the given <paramref name="memory" />.
    /// </exception>
    public Operations(MemorySafeBuffer memory)
    {
        Handle = Sdl.ReadWriteFromConstMemory(memory, (int)memory.ByteLength);
        if (Handle.IsClosed || Handle.IsInvalid)
        {
            throw new ReadWriteOperationsException(Sdl.GetError());
        }
    }

    /// <summary>Create an empty, unpopulated read/write operations class.</summary>
    /// <exception cref="ReadWriteOperationsException">When SDL is unable to create the read/write operations.</exception>
    /// <remarks>
    ///     You may want to specify the <see cref="CloseCallback" />, <see cref="ReadCallback" />,
    ///     <see cref="SeekCallback" />, <see cref="SizeCallback" /> and <see cref="WriteCallback" /> callbacks if using this
    ///     constructor.
    /// </remarks>
    public Operations()
    {
        Handle = Sdl.AllocateReadWrite();
        if (Handle.IsClosed || Handle.IsInvalid)
        {
            throw new ReadWriteOperationsException(Sdl.GetError());
        }
    }

    /// <summary>Get the internal <see cref="ReadWriteOperationsSafeHandle" />.</summary>
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global",
        Justification = "This is meant to be accessible from the library.")]
    public ReadWriteOperationsSafeHandle Handle { get; private set; }

    /// <summary>Read/Write a byte from/into the data stream.</summary>
    /// <exception cref="ReadWriteOperationsException">When SDL is unable to read/write the byte.</exception>
    public byte Unsigned8
    {
        get
        {
            ReadWriteOperationsSafeHandle context = Handle;
            byte result = Sdl.ReadUnsigned8(ref context);
            Handle = context;
            if (result == 0)
            {
                throw new ReadWriteOperationsException(Sdl.GetError());
            }

            return result;
        }
        set
        {
            ReadWriteOperationsSafeHandle context = Handle;
            CULong result = Sdl.WriteUnsigned8(ref context, value);
            Handle = context;
            if (result.Value == 0)
            {
                throw new ReadWriteOperationsException(Sdl.GetError());
            }
        }
    }

    /// <summary>Read/Write 16 bits of little-endian data from/into the data stream.</summary>
    /// <exception cref="ReadWriteOperationsException">When SDL is unable to read/write the bytes.</exception>
    /// <remarks>SDL byte swaps the data only if necessary, so the data returned will be in the native byte order.</remarks>
    public ushort LittleEndian16
    {
        get
        {
            ReadWriteOperationsSafeHandle context = Handle;
            ushort result = Sdl.ReadLittleEndian16(ref context);
            Handle = context;
            if (result == 0)
            {
                throw new ReadWriteOperationsException(Sdl.GetError());
            }

            return result;
        }
        set
        {
            ReadWriteOperationsSafeHandle context = Handle;
            CULong result = Sdl.WriteLittleEndian16(ref context, value);
            Handle = context;
            if (result.Value == 0)
            {
                throw new ReadWriteOperationsException(Sdl.GetError());
            }
        }
    }

    /// <summary>Read/Write 16 bits of big-endian data from/into the data stream.</summary>
    /// <exception cref="ReadWriteOperationsException">When SDL is unable to read/write the bytes.</exception>
    /// <remarks>SDL byte swaps the data only if necessary, so the data returned will be in the native byte order.</remarks>
    public ushort BigEndian16
    {
        get
        {
            ReadWriteOperationsSafeHandle context = Handle;
            ushort result = Sdl.ReadBigEndian16(ref context);
            Handle = context;
            if (result == 0)
            {
                throw new ReadWriteOperationsException(Sdl.GetError());
            }

            return result;
        }
        set
        {
            ReadWriteOperationsSafeHandle context = Handle;
            CULong result = Sdl.WriteBigEndian16(ref context, value);
            Handle = context;
            if (result.Value == 0)
            {
                throw new ReadWriteOperationsException(Sdl.GetError());
            }
        }
    }

    /// <summary>Read/Write 32 bits of little-endian data from/into the data stream.</summary>
    /// <exception cref="ReadWriteOperationsException">When SDL is unable to read/write the bytes.</exception>
    /// <remarks>SDL byte swaps the data only if necessary, so the data returned will be in the native byte order.</remarks>
    public uint LittleEndian32
    {
        get
        {
            ReadWriteOperationsSafeHandle context = Handle;
            uint result = Sdl.ReadLittleEndian32(ref context);
            Handle = context;
            if (result == 0)
            {
                throw new ReadWriteOperationsException(Sdl.GetError());
            }

            return result;
        }
        set
        {
            ReadWriteOperationsSafeHandle context = Handle;
            CULong result = Sdl.WriteLittleEndian32(ref context, value);
            Handle = context;
            if (result.Value == 0)
            {
                throw new ReadWriteOperationsException(Sdl.GetError());
            }
        }
    }

    /// <summary>Read/Write 32 bits of big-endian data from/into the data stream.</summary>
    /// <exception cref="ReadWriteOperationsException">When SDL is unable to read/write the bytes.</exception>
    /// <remarks>SDL byte swaps the data only if necessary, so the data returned will be in the native byte order.</remarks>
    public uint BigEndian32
    {
        get
        {
            ReadWriteOperationsSafeHandle context = Handle;
            uint result = Sdl.ReadBigEndian32(ref context);
            Handle = context;
            if (result == 0)
            {
                throw new ReadWriteOperationsException(Sdl.GetError());
            }

            return result;
        }
        set
        {
            ReadWriteOperationsSafeHandle context = Handle;
            CULong result = Sdl.WriteBigEndian32(ref context, value);
            Handle = context;
            if (result.Value == 0)
            {
                throw new ReadWriteOperationsException(Sdl.GetError());
            }
        }
    }

    /// <summary>Read/Write 64 bits of little-endian data from/into the data stream.</summary>
    /// <exception cref="ReadWriteOperationsException">When SDL is unable to read/write the bytes.</exception>
    /// <remarks>SDL byte swaps the data only if necessary, so the data returned will be in the native byte order.</remarks>
    public ulong LittleEndian64
    {
        get
        {
            ReadWriteOperationsSafeHandle context = Handle;
            ulong result = Sdl.ReadLittleEndian64(ref context);
            Handle = context;
            if (result == 0)
            {
                throw new ReadWriteOperationsException(Sdl.GetError());
            }

            return result;
        }
        set
        {
            ReadWriteOperationsSafeHandle context = Handle;
            CULong result = Sdl.WriteLittleEndian64(ref context, value);
            Handle = context;
            if (result.Value == 0)
            {
                throw new ReadWriteOperationsException(Sdl.GetError());
            }
        }
    }

    /// <summary>Read/Write 64 bits of big-endian data from/into the data stream.</summary>
    /// <exception cref="ReadWriteOperationsException">When SDL is unable to read/write the bytes.</exception>
    /// <remarks>SDL byte swaps the data only if necessary, so the data returned will be in the native byte order.</remarks>
    public ulong BigEndian64
    {
        get
        {
            ReadWriteOperationsSafeHandle context = Handle;
            ulong result = Sdl.ReadBigEndian64(ref context);
            Handle = context;
            if (result == 0)
            {
                throw new ReadWriteOperationsException(Sdl.GetError());
            }

            return result;
        }
        set
        {
            ReadWriteOperationsSafeHandle context = Handle;
            CULong result = Sdl.WriteBigEndian64(ref context, value);
            Handle = context;
            if (result.Value == 0)
            {
                throw new ReadWriteOperationsException(Sdl.GetError());
            }
        }
    }

    /// <summary>Get the size of the read/write stream.</summary>
    /// <returns>A <see cref="long" /> with the size of the stream.</returns>
    /// <exception cref="ReadWriteOperationsException">When SDL is unable to get the size of the stream.</exception>
    public long Size()
    {
        ReadWriteOperationsSafeHandle context = Handle;
        long result = Sdl.ReadWriteSize(ref context);
        Handle = context;
        if (result < 0)
        {
            throw new ReadWriteOperationsException(Sdl.GetError(), (int)result);
        }

        return result;
    }

    /// <summary>Seek within the read/write stream.</summary>
    /// <param name="offset">A <see cref="long" /> with the offset to seek from relative to <paramref name="whence" />.</param>
    /// <param name="whence">The <see cref="SeekOrigin" />.</param>
    /// <returns>The final offset of in the stream after seeking.</returns>
    /// <exception cref="ReadWriteOperationsException">When SDL is unable to seek in the data stream.</exception>
    public long Seek(long offset, SeekOrigin whence)
    {
        ReadWriteOperationsSafeHandle context = Handle;
        long result = Sdl.ReadWriteSeek(ref context, offset, whence);
        Handle = context;
        if (result < 0)
        {
            throw new ReadWriteOperationsException(Sdl.GetError(), (int)result);
        }

        return result;
    }

    /// <summary>Get the current read/write offset in the data stream.</summary>
    /// <returns>A <see cref="long" /> with the current offset.</returns>
    /// <exception cref="ReadWriteOperationsException">When SDL is unable to determine the current offset.</exception>
    /// <remarks>This is equivalent to calling <code>Seek(0, SeekOrigin.Current);</code></remarks>
    public long Tell()
    {
        ReadWriteOperationsSafeHandle context = Handle;
        long result = Sdl.ReadWriteTell(ref context);
        Handle = context;
        if (result < 0)
        {
            throw new ReadWriteOperationsException(Sdl.GetError(), (int)result);
        }

        return result;
    }

    /// <summary>Read from the data source.</summary>
    /// <param name="ptr">An initialized <see cref="MemorySafeBuffer" /> to read into.</param>
    /// <param name="maximumNumberOfObjects">A <see cref="uint" /> with the maximum number of objects to read.</param>
    /// <returns>The number of objects read.</returns>
    /// <exception cref="ReadWriteOperationsException">When SDL is unable to read the data source.</exception>
    /// <remarks>
    ///     SDL may be unable to read the data source because the end of file has been reached. This may be an exception
    ///     you wish to handle if you expect reaching an EOF naturally.
    /// </remarks>
    public uint Read(ref MemorySafeBuffer ptr, uint maximumNumberOfObjects)
    {
        ReadWriteOperationsSafeHandle context = Handle;
        CULong result = Sdl.ReadWriteRead(ref context, ref ptr, new CULong((nuint)ptr.ByteLength),
            new CULong(maximumNumberOfObjects));

        Handle = context;
        if (result.Value == 0)
        {
            throw new ReadWriteOperationsException(Sdl.GetError());
        }

        return (uint)result.Value;
    }

    /// <summary>Write to the data source.</summary>
    /// <param name="ptr">The <see cref="MemorySafeBuffer" /> to write.</param>
    /// <param name="numberOfObjects">A <see cref="uint" /> with the number of objects to write.</param>
    /// <returns>A <see cref="uint" /> with the number of objects written.</returns>
    /// <exception cref="ReadWriteOperationsException">
    ///     When SDL is unable to write the given number of objects (when the
    ///     returned value is under the <paramref name="numberOfObjects" />).
    /// </exception>
    public uint Write(MemorySafeBuffer ptr, uint numberOfObjects)
    {
        ReadWriteOperationsSafeHandle context = Handle;
        CULong result = Sdl.ReadWriteWrite(ref context, ptr, new CULong((nuint)ptr.ByteLength),
            new CULong(numberOfObjects));

        Handle = context;
        if ((uint)result.Value < numberOfObjects)
        {
            throw new ReadWriteOperationsException(Sdl.GetError());
        }

        return (uint)result.Value;
    }

    /// <summary>Close and free the internally allocated read/write data stream.</summary>
    /// <exception cref="ReadWriteOperationsException">When SDL is unable to close the stream.</exception>
    /// <remarks>Note that an exception will still leave the internal <see cref="Handle" /> as invalid.</remarks>
    public void Close()
    {
        ReadWriteOperationsSafeHandle context = Handle;
        int errorCode = Sdl.ReadWriteClose(ref context);
        Handle = context;
        if (errorCode < 0)
        {
            throw new ReadWriteOperationsException(Sdl.GetError(), errorCode);
        }
    }

    /// <summary>Read the data from a read/write stream file.</summary>
    /// <param name="src">The <see cref="Operations" /> read/write data stream.</param>
    /// <param name="freeSrc">Whether to free the internal data stream handle or not.</param>
    /// <returns>A <see cref="MemorySafeBuffer" /> with the data of the read/write stream.</returns>
    /// <exception cref="ReadWriteOperationsException">When SDL is unable to read the stream file.</exception>
    public static MemorySafeBuffer ReadFile(ref Operations src, bool freeSrc)
    {
        ReadWriteOperationsSafeHandle context = src.Handle;
        MemorySafeBuffer result = Sdl.LoadFileReadWrite(ref context, IntPtr.Zero, freeSrc);
        src.Handle = context;
        if (result.IsClosed || result.IsInvalid)
        {
            throw new ReadWriteOperationsException(Sdl.GetError());
        }

        return result;
    }

    /// <summary>Read the data from a read/write stream file.</summary>
    /// <param name="src">The <see cref="Operations" /> read/write data stream.</param>
    /// <param name="size">An <see langword="out" /> parameter with a <see cref="uint" /> with the data size.</param>
    /// <param name="freeSrc">Whether to free the internal data stream handle or not.</param>
    /// <returns>A <see cref="MemorySafeBuffer" /> with the data of the read/write stream.</returns>
    /// <exception cref="ReadWriteOperationsException">When SDL is unable to read the stream file.</exception>
    public static MemorySafeBuffer ReadFile(ref Operations src, out uint size, bool freeSrc)
    {
        ReadWriteOperationsSafeHandle context = src.Handle;
        MemorySafeBuffer result = Sdl.LoadFileReadWrite(ref context, out CULong nativeSize, freeSrc);
        src.Handle = context;
        size = (uint)nativeSize.Value;
        if (result.IsClosed || result.IsInvalid)
        {
            throw new ReadWriteOperationsException(Sdl.GetError());
        }

        return result;
    }

    /// <summary>Read the data from a file.</summary>
    /// <param name="file">A <see cref="string" /> to the file path.</param>
    /// <returns>A <see cref="MemorySafeBuffer" /> with the data of the file.</returns>
    /// <exception cref="ReadWriteOperationsException">When SDL is unable to read the file.</exception>
    public static MemorySafeBuffer ReadFile(string file)
    {
        MemorySafeBuffer result = Sdl.LoadFile(file, IntPtr.Zero);
        if (result.IsClosed || result.IsInvalid)
        {
            throw new ReadWriteOperationsException(Sdl.GetError());
        }

        return result;
    }

    /// <summary>Read the data from a file.</summary>
    /// <param name="file">A <see cref="string" /> to the file path.</param>
    /// <param name="size">An <see langword="out" /> parameter with a <see cref="uint" /> with the file data size.</param>
    /// <returns>A <see cref="MemorySafeBuffer" /> with the data of the file.</returns>
    /// <exception cref="ReadWriteOperationsException">When SDL is unable to read the file.</exception>
    public static MemorySafeBuffer ReadFile(string file, out uint size)
    {
        MemorySafeBuffer result = Sdl.LoadFile(file, out CULong nativeSize);
        size = (uint)nativeSize.Value;
        if (result.IsClosed || result.IsInvalid)
        {
            throw new ReadWriteOperationsException(Sdl.GetError());
        }

        return result;
    }
}