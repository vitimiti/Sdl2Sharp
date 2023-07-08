using Xunit;

namespace Sdl2Sharp.UnitTests;

[Collection(Collections.SubsystemsCollection)]
public class SubsystemsTests
{
    [Fact]
    public void TimerSubsystemWorks()
    {
        using (new Subsystem(SubsystemFlags.Timer))
        {
            Assert.Equal(SubsystemFlags.Timer, Subsystem.InitializedSubsystems);
        }

        Assert.Equal(SubsystemFlags.None, Subsystem.InitializedSubsystems);
    }

    [Fact]
    public void AudioSubsystemWorks()
    {
        using (new Subsystem(SubsystemFlags.Audio))
        {
            Assert.Equal(SubsystemFlags.Audio | SubsystemFlags.Events, Subsystem.InitializedSubsystems);
        }

        Assert.Equal(SubsystemFlags.None, Subsystem.InitializedSubsystems);
    }

    [Fact]
    public void VideoSubsystemWorks()
    {
        using (new Subsystem(SubsystemFlags.Video))
        {
            Assert.Equal(SubsystemFlags.Video | SubsystemFlags.Events, Subsystem.InitializedSubsystems);
        }

        Assert.Equal(SubsystemFlags.None, Subsystem.InitializedSubsystems);
    }

    [Fact]
    public void JoystickSubsystemWorks()
    {
        using (new Subsystem(SubsystemFlags.Joystick))
        {
            Assert.Equal(SubsystemFlags.Joystick | SubsystemFlags.Events, Subsystem.InitializedSubsystems);
        }

        Assert.Equal(SubsystemFlags.None, Subsystem.InitializedSubsystems);
    }

    [Fact]
    public void HapticSubsystemWorks()
    {
        using (new Subsystem(SubsystemFlags.Haptic))
        {
            Assert.Equal(SubsystemFlags.Haptic, Subsystem.InitializedSubsystems);
        }

        Assert.Equal(SubsystemFlags.None, Subsystem.InitializedSubsystems);
    }

    [Fact]
    public void GameControllerSubsystemWorks()
    {
        using (new Subsystem(SubsystemFlags.GameController))
        {
            Assert.Equal(SubsystemFlags.GameController | SubsystemFlags.Joystick | SubsystemFlags.Events,
                Subsystem.InitializedSubsystems);
        }

        Assert.Equal(SubsystemFlags.None, Subsystem.InitializedSubsystems);
    }

    [Fact]
    public void SensorSubsystemWorks()
    {
        using (new Subsystem(SubsystemFlags.Sensor))
        {
            Assert.Equal(SubsystemFlags.Sensor | SubsystemFlags.Events, Subsystem.InitializedSubsystems);
        }

        Assert.Equal(SubsystemFlags.None, Subsystem.InitializedSubsystems);
    }

    [Fact]
    public void EverythingSubsystemWorks()
    {
        using (new Subsystem(SubsystemFlags.Everything))
        {
            Assert.Equal(SubsystemFlags.Everything, Subsystem.InitializedSubsystems);
            Assert.Equal(
                SubsystemFlags.Timer | SubsystemFlags.Audio | SubsystemFlags.Video | SubsystemFlags.Events |
                SubsystemFlags.Joystick | SubsystemFlags.Haptic | SubsystemFlags.GameController | SubsystemFlags.Sensor,
                Subsystem.InitializedSubsystems);
        }

        Assert.Equal(SubsystemFlags.None, Subsystem.InitializedSubsystems);
    }

    [Fact]
    public void StartingAfterInitializationWorks()
    {
        using (Subsystem subsystem = new(SubsystemFlags.Timer))
        {
            subsystem.Start(SubsystemFlags.Video);
            Assert.Equal(SubsystemFlags.Timer | SubsystemFlags.Video | SubsystemFlags.Events,
                Subsystem.InitializedSubsystems);
        }

        Assert.Equal(SubsystemFlags.None, Subsystem.InitializedSubsystems);
    }

    [Fact]
    public void StoppingAfterInitializationWorks()
    {
        using (Subsystem subsystem = new(SubsystemFlags.Timer | SubsystemFlags.Video))
        {
            subsystem.Stop(SubsystemFlags.Timer);
            Assert.Equal(SubsystemFlags.Events | SubsystemFlags.Video, Subsystem.InitializedSubsystems);
        }

        Assert.Equal(SubsystemFlags.None, Subsystem.InitializedSubsystems);
    }
}