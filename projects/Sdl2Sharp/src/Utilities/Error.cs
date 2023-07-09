using Sdl2Sharp.Interop;

namespace Sdl2Sharp.Utilities;

/// <summary>A class to retrieve error information.</summary>
public static class Error
{
    /// <summary>Get the last SDL error.</summary>
    /// <returns>A <see cref="string" /> with the last error.</returns>
    /// <remarks>This is only to be used with functions that may error but don't throw exceptions, such as pointer callbacks.</remarks>
    public static string DangerousGetLast()
    {
        return Sdl.GetError();
    }
}