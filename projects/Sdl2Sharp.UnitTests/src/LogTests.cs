using System;
using System.Diagnostics.CodeAnalysis;

using Sdl2Sharp.Utilities;
using Sdl2Sharp.Utilities.Log;

using Xunit;

namespace Sdl2Sharp.UnitTests;

public class LogTests
{
    private const string LogText = "Test";

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

    [Fact]
    public void SettingAndResettingAllLogPrioritiesWorks()
    {
        Functions.SetAllLogPriorities(Priority.Error);
        Array values = Enum.GetValues<MyCategories>();
        foreach (MyCategories category in values)
        {
            Assert.Equal(Priority.Error, Functions.GetLogPriority(category));
        }

        Functions.ResetLogPriorities();
        foreach (MyCategories category in values)
        {
            Assert.Equal(Priority.Critical, Functions.GetLogPriority(category));
        }
    }

    [Fact]
    public void SettingOneLogPriorityWorks()
    {
        Functions.SetLogPriority(MyCategories.First, Priority.Error);
        Array values = Enum.GetValues<MyCategories>();
        foreach (MyCategories category in values)
        {
            switch (category)
            {
                case MyCategories.First:
                    Assert.Equal(Priority.Error, Functions.GetLogPriority(category));
                    break;
                case MyCategories.Second:
                case MyCategories.Third:
                default:
                    Assert.Equal(Priority.Critical, Functions.GetLogPriority(category));
                    break;
            }
        }

        Functions.ResetLogPriorities();
    }

    [Fact]
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local",
        Justification = "Required for assertions")]
    public void FunctionLogWorks()
    {
        Functions.SetLogOutputFunction((data, category, priority, message) =>
        {
            Assert.Null(data);
            Assert.Equal(Category.Application, (Category)category);
            Assert.Equal(Priority.Information, priority);
            Assert.Equal(LogText, message);
        }, new NullVoidPointer());

        Functions.Log(LogText);
    }

    [Fact]
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local",
        Justification = "Required for assertions")]
    public void FunctionLogVerboseWorks()
    {
        Functions.SetLogOutputFunction((data, category, priority, message) =>
        {
            Assert.Null(data);
            Assert.Equal(Category.Application, (Category)category);
            Assert.Equal(Priority.Verbose, priority);
            Assert.Equal(LogText, message);
        }, new NullVoidPointer());

        Functions.LogVerbose(Category.Application, LogText);
    }

    [Fact]
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local",
        Justification = "Required for assertions")]
    public void FunctionLogDebugWorks()
    {
        Functions.SetLogOutputFunction((data, category, priority, message) =>
        {
            Assert.Null(data);
            Assert.Equal(Category.Application, (Category)category);
            Assert.Equal(Priority.Debug, priority);
            Assert.Equal(LogText, message);
        }, new NullVoidPointer());

        Functions.LogDebug(Category.Application, LogText);
    }

    [Fact]
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local",
        Justification = "Required for assertions")]
    public void FunctionLogInformationWorks()
    {
        Functions.SetLogOutputFunction((data, category, priority, message) =>
        {
            Assert.Null(data);
            Assert.Equal(Category.Application, (Category)category);
            Assert.Equal(Priority.Information, priority);
            Assert.Equal(LogText, message);
        }, new NullVoidPointer());

        Functions.LogInformation(Category.Application, LogText);
    }

    [Fact]
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local",
        Justification = "Required for assertions")]
    public void FunctionLogWarningWorks()
    {
        Functions.SetLogOutputFunction((data, category, priority, message) =>
        {
            Assert.Null(data);
            Assert.Equal(Category.Application, (Category)category);
            Assert.Equal(Priority.Warning, priority);
            Assert.Equal(LogText, message);
        }, new NullVoidPointer());

        Functions.LogWarning(Category.Application, LogText);
    }

    [Fact]
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local",
        Justification = "Required for assertions")]
    public void FunctionLogErrorWorks()
    {
        Functions.SetLogOutputFunction((data, category, priority, message) =>
        {
            Assert.Null(data);
            Assert.Equal(Category.Application, (Category)category);
            Assert.Equal(Priority.Error, priority);
            Assert.Equal(LogText, message);
        }, new NullVoidPointer());

        Functions.LogError(Category.Application, LogText);
    }

    [Fact]
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local",
        Justification = "Required for assertions")]
    public void FunctionLogCriticalWorks()
    {
        Functions.SetLogOutputFunction((data, category, priority, message) =>
        {
            Assert.Null(data);
            Assert.Equal(Category.Application, (Category)category);
            Assert.Equal(Priority.Critical, priority);
            Assert.Equal(LogText, message);
        }, new NullVoidPointer());

        Functions.LogCritical(Category.Application, LogText);
    }

    [Fact]
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local",
        Justification = "Required for assertions")]
    public void FunctionLogMessageWorks()
    {
        Functions.SetLogOutputFunction((data, category, priority, message) =>
        {
            Assert.Null(data);
            Assert.Equal(Category.Application, (Category)category);
            Assert.Equal(Priority.Information, priority);
            Assert.Equal(LogText, message);
        }, new NullVoidPointer());

        Functions.LogMessage(Category.Application, Priority.Information, LogText);
    }

    private enum MyCategories
    {
        First = Category.Custom,
        Second,
        Third
    }
}