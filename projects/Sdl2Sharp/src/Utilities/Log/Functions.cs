using System.Runtime.InteropServices;

using Sdl2Sharp.CustomMarshalers;
using Sdl2Sharp.Interop;

namespace Sdl2Sharp.Utilities.Log;

/// <summary>A class to log through SDL's logging system.</summary>
public static class Functions
{
    /// <summary>The prototype for the log output callback function.</summary>
    /// <param name="userData">An <see cref="object" /> to pass as user data to the log function.</param>
    /// <param name="category">An <see cref="int" /> with the category, either from <see cref="Category" /> or a custom one.</param>
    /// <param name="priority">The <see cref="Priority" /> for the logging function.</param>
    /// <param name="message">A <see cref="string" /> with the message to log.</param>
    /// <remarks>
    ///     <para>This function is called by SDL when there is new text to be logged.</para>
    ///     <para>
    ///         To pass in no <paramref name="userData" /> (or a NULL/nullptr void pointer in C), you may use
    ///         <see cref="NullVoidPointer" />.
    ///     </para>
    /// </remarks>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public delegate void LogOutputFunction(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VoidPointerCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        object userData, int category, Priority priority,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(SdlStringCustomMarshaler))]
        string message);

    /// <summary>Set the priority of all log categories.</summary>
    /// <param name="priority">The <see cref="Priority" /> to assign.</param>
    public static void SetAllLogPriorities(Priority priority)
    {
        Sdl.LogSetAllPriority(priority);
    }

    /// <summary>Set the priority of a particular log category.</summary>
    /// <param name="category">An <see cref="int" /> with the category, either from <see cref="Category" /> or a custom one.</param>
    /// <param name="priority">The <see cref="Priority" /> to assign.</param>
    public static void SetLogPriority(int category, Priority priority)
    {
        Sdl.LogSetPriority(category, priority);
    }

    /// <summary>Get the <see cref="Priority" /> of a particular log category.</summary>
    /// <param name="category">An <see cref="int" /> with the category, either from <see cref="Category" /> or a custom one.</param>
    /// <returns>The <see cref="Priority" /> of the <paramref name="category" />.</returns>
    public static Priority GetLogPriority(int category)
    {
        return Sdl.LogGetPriority(category);
    }

    /// <summary>
    ///     Log a <paramref name="message" /> with category <see cref="Category.Application" /> and priority
    ///     <see cref="Priority.Information" />.
    /// </summary>
    /// <param name="message">A <see cref="string" /> with the message to log.</param>
    public static void Log(string message)
    {
        Sdl.Log(message);
    }

    /// <summary>Log a <paramref name="message" /> with priority <see cref="Priority.Verbose" />.</summary>
    /// <param name="category">An <see cref="int" /> with the category, either from <see cref="Category" /> or a custom one.</param>
    /// <param name="message">A <see cref="string" /> with the message to log.</param>
    public static void LogVerbose(int category, string message)
    {
        Sdl.LogVerbose(category, message);
    }

    /// <summary>Log a <paramref name="message" /> with priority <see cref="Priority.Debug" />.</summary>
    /// <param name="category">An <see cref="int" /> with the category, either from <see cref="Category" /> or a custom one.</param>
    /// <param name="message">A <see cref="string" /> with the message to log.</param>
    public static void LogDebug(int category, string message)
    {
        Sdl.LogDebug(category, message);
    }

    /// <summary>Log a <paramref name="message" /> with priority <see cref="Priority.Information" />.</summary>
    /// <param name="category">An <see cref="int" /> with the category, either from <see cref="Category" /> or a custom one.</param>
    /// <param name="message">A <see cref="string" /> with the message to log.</param>
    public static void LogInformation(int category, string message)
    {
        Sdl.LogInformation(category, message);
    }

    /// <summary>Log a <paramref name="message" /> with priority <see cref="Priority.Warning" />.</summary>
    /// <param name="category">An <see cref="int" /> with the category, either from <see cref="Category" /> or a custom one.</param>
    /// <param name="message">A <see cref="string" /> with the message to log.</param>
    public static void LogWarning(int category, string message)
    {
        Sdl.LogWarning(category, message);
    }

    /// <summary>Log a <paramref name="message" /> with priority <see cref="Priority.Error" />.</summary>
    /// <param name="category">An <see cref="int" /> with the category, either from <see cref="Category" /> or a custom one.</param>
    /// <param name="message">A <see cref="string" /> with the message to log.</param>
    public static void LogError(int category, string message)
    {
        Sdl.LogError(category, message);
    }

    /// <summary>Log a <paramref name="message" /> with priority <see cref="Priority.Critical" />.</summary>
    /// <param name="category">An <see cref="int" /> with the category, either from <see cref="Category" /> or a custom one.</param>
    /// <param name="message">A <see cref="string" /> with the message to log.</param>
    public static void LogCritical(int category, string message)
    {
        Sdl.LogCritical(category, message);
    }

    /// <summary>Log a <paramref name="message" />.</summary>
    /// <param name="category">An <see cref="int" /> with the category, either from <see cref="Category" /> or a custom one.</param>
    /// <param name="priority">A <see cref="Priority" /> with the message priority.</param>
    /// <param name="message">A <see cref="string" /> with the message to log.</param>
    public static void LogMessage(int category, Priority priority, string message)
    {
        Sdl.LogMessage(category, priority, message);
    }

    /// <summary>Get the current <see cref="LogOutputFunction" /> and its user data.</summary>
    /// <param name="callback">
    ///     An <see langword="out" /> parameter filled in with the current <see cref="LogOutputFunction" />.
    /// </param>
    /// <param name="userData">
    ///     An <see langword="out" /> parameter filled in with the current <see cref="object" /> holding the
    ///     user data.
    /// </param>
    /// <remarks>
    ///     You may want to check if <paramref name="userData" /> is <see langword="null" /> before using it if you set the
    ///     user data in <see cref="SetLogOutputFunction" /> to be a new <see cref="NullVoidPointer()" />.
    /// </remarks>
    public static void GetLogOutputFunction(out LogOutputFunction callback, out object userData)
    {
        Sdl.LogGetOutputFunction(out callback, out userData);
    }

    /// <summary>Replace the default or current log output function with one of your own.</summary>
    /// <param name="callback">A <see cref="LogOutputFunction" /> with the function to call back.</param>
    /// <param name="userData">An <see cref="object" /> with the user data to pass to the callback.</param>
    /// <exception cref="ArgumentException">
    ///     When an empty new <see cref="object()" /> was passed as a null pointer representation instead of a new
    ///     <see cref="NullVoidPointer()" />.
    /// </exception>
    /// <remarks>
    ///     Pass a new <see cref="NullVoidPointer()" /> as the <paramref name="userData" /> if you don't want to pass any
    ///     data.
    /// </remarks>
    public static void SetLogOutputFunction(LogOutputFunction callback, object userData)
    {
        if (userData.GetType() == typeof(object))
        {
            throw new ArgumentException(
                $"Empty object detected in {nameof(userData)}. If you don't want to pass any data, please use the {nameof(NullVoidPointer)} class instead of the {nameof(Object)} class.");
        }

        Sdl.LogSetOutputFunction(callback, userData);
    }
}