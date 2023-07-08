using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Sdl2Sharp.Exceptions;

/// <summary>The exception type for SDL initialization exceptions.</summary>
[Serializable]
public sealed class ClipboardException : ExternalException
{
    /// <summary>Initializes a new instance of the <see langword="ClipboardException" /> class with default properties.</summary>
    public ClipboardException()
    {
    }

    /// <summary>Initializes a new instance of the <see langword="ClipboardException" /> class with a specified error message.</summary>
    /// <param name="message">The error message that specifies the reason for the exception.</param>
    public ClipboardException(string? message) : base(message)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ClipboardException" /> class with a specified error message and a
    ///     reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="inner">
    ///     The exception that is the cause of the current exception. If the <paramref name="inner" />
    ///     parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that
    ///     handles the inner exception.
    /// </param>
    public ClipboardException(string? message, Exception? inner) : base(message, inner)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see langword="ClipboardException" /> class with a specified error message
    ///     and the HRESULT of the error.
    /// </summary>
    /// <param name="message">The error message that specifies the reason for the exception.</param>
    /// <param name="errorCode">The HRESULT of the error.</param>
    public ClipboardException(string? message, int errorCode) : base(message, errorCode)
    {
    }

    /// <summary>Initializes a new instance of the <see langword="ClipboardException" /> class from serialization data.</summary>
    /// <param name="info">The object that holds the serialized object data.</param>
    /// <param name="context">The contextual information about the source or destination.</param>
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="info" /> is <see langword="null" />.
    /// </exception>
    public ClipboardException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}