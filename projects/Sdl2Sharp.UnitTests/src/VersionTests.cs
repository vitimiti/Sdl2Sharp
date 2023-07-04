using System;

using Sdl2Sharp.Utilities;

using Xunit;

namespace Sdl2Sharp.UnitTests;

public class VersionTests
{
    [Fact]
    public void VersionIsCorrect()
    {
        Version expected = new(2, 0, 20);
        Assert.Equal(expected, NativeDllInformation.Version);
    }
}