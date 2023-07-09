using System.Diagnostics.CodeAnalysis;

using Sdl2Sharp.Utilities;

using Xunit;

namespace Sdl2Sharp.UnitTests;

[Collection(Collections.SubsystemsCollection)]
public class TimerTests
{
    [Fact]
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local",
        Justification = "Required for assertions")]
    public void CreatingATimerWithoutParameterWorks()
    {
        const uint expectedInterval = 1U;
        using (new Subsystem(SubsystemFlags.Timer))
        {
            using (new Timer(expectedInterval, (interval, parameter) =>
                   {
                       Assert.Equal(expectedInterval, interval);
                       Assert.Null(parameter);
                       return 0;
                   }, new NullVoidPointer()))
            {
            }
        }
    }

    [Fact]
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local",
        Justification = "Required for assertions")]
    public void CreatingATimerWithParameterWoks()
    {
        const uint expectedInterval = 1U;
        const string expectedParameter = "Test";
        using (new Subsystem(SubsystemFlags.Timer))
        {
            using (new Timer(expectedInterval, (interval, parameter) =>
                   {
                       Assert.Equal(expectedInterval, interval);
                       Assert.NotNull(parameter);
                       Assert.Equal(expectedParameter, parameter as string);
                       return 0;
                   }, expectedParameter))
            {
            }
        }
    }
}