namespace Sdl2Sharp.Utilities.ReadWriteOperations;

/// <summary>Types of read/write operations.</summary>
public enum Type : uint
{
    /// <summary>Unknown stream type.</summary>
    Unknown,

    /// <summary>Win32 file.</summary>
    WinFile,

    /// <summary>Stdio file.</summary>
    StdFile,

    /// <summary>Android asset.</summary>
    JniFile,

    /// <summary>Memory stream.</summary>
    Memory,

    /// <summary>Read-Only memory stream.</summary>
    MemoryReadOnly,

    /// <summary>PS Vita file.</summary>
    VitaFile
}