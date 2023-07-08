using Sdl2Sharp.Exceptions;
using Sdl2Sharp.Interop;

namespace Sdl2Sharp.Utilities;

/// <summary>A class to manage the clipboard.</summary>
public static class Clipboard
{
    /// <summary>Get/Set a <see cref="string" /> with the clipboard text</summary>
    /// <exception cref="ClipboardException">When SDL is unable to get the clipboard text.</exception>
    /// <exception cref="ClipboardException">
    ///     When SDL is unable to set the clipboard text. Normally because the
    ///     <see cref="SubsystemFlags.Video" /> subsystem wasn't initialized.
    /// </exception>
    /// <remarks>Use <see cref="HasText" /> to prevent exceptions during value getting.</remarks>
    public static string Text
    {
        get
        {
            string str = Sdl.GetClipboardText();
            return str == string.Empty ? throw new ClipboardException(Sdl.GetError()) : str;
        }
        set
        {
            int errorCode = Sdl.SetClipboardText(value);
            if (errorCode < 0)
            {
                throw new ClipboardException(Sdl.GetError(), errorCode);
            }
        }
    }

    /// <summary>Whether the clipboard has text or not.</summary>
    public static bool HasText => Sdl.HasClipboardText();
}