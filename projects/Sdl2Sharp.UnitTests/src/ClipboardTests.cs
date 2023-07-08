using Sdl2Sharp.Exceptions;
using Sdl2Sharp.Utilities;

using Xunit;

namespace Sdl2Sharp.UnitTests;

[Collection(Collections.SubsystemsCollection)]
public class ClipboardTests
{
    private const string TestText = "Test";

    private static void ResetText()
    {
        Clipboard.Text = string.Empty;
    }

    [Fact]
    public void SettingClipboardTextWithoutVideoSubsystemThrowsException()
    {
        Assert.Throws<ClipboardException>(() =>
        {
            Clipboard.Text = TestText;
        });
    }

    [Fact]
    public void GettingEmptyClipboardTextThrowsException()
    {
        Assert.Throws<ClipboardException>(() =>
        {
            using (new Subsystem(SubsystemFlags.Video))
            {
                ResetText();
                return Clipboard.Text;
            }
        });
    }

    [Fact]
    public void SettingClipboardTextWorks()
    {
        using (new Subsystem(SubsystemFlags.Video))
        {
            ResetText();
            Clipboard.Text = TestText;
            Assert.Equal(TestText, Clipboard.Text);
        }
    }

    [Fact]
    public void CheckingClipboardHasTextWorks()
    {
        using (new Subsystem(SubsystemFlags.Video))
        {
            ResetText();
            Assert.False(Clipboard.HasText);
            Clipboard.Text = TestText;
            Assert.True(Clipboard.HasText);
        }
    }
}