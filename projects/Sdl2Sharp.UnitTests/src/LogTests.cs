using Sdl2Sharp.Utilities;
using Sdl2Sharp.Utilities.Log;

using Xunit;

namespace Sdl2Sharp.UnitTests;

public class LogTests
{
    private static void Callback(object userData, int category, Priority priority, string message)
    {
    }

    [Fact]
    public void SettingLogOutputFunctionCallbackWithEmptyObjectWorks()
    {
        Functions.SetLogOutputFunction(Callback, new NullVoidPointer());
        Functions.GetLogOutputFunction(out _, out object userData);
        Assert.Null(userData);
    }

    [Fact]
    public void SettingLogOutputFunctionCallbackWithValidObjectWorks()
    {
        const string strTest = "Test";
        Functions.SetLogOutputFunction(Callback, strTest);
        Functions.GetLogOutputFunction(out _, out object userData);
        Assert.True(userData is string);
        Assert.Equal(strTest, userData as string);
    }
}