using System;
using System.Runtime.InteropServices;

using Sdl2Sharp.Utilities;

using Xunit;

namespace Sdl2Sharp.UnitTests;

public class PlatformTests
{
    [Fact]
    public void NameIsCorrect()
    {
        string expected = String.Empty;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            expected = "Windows";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            expected = "OSX";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            expected = "Linux";
        }

        Assert.Equal(expected, Platform.Name);
    }
}