namespace Sdl2Sharp.Utilities.Log;

/// <summary>The predefine log categories.</summary>
/// <remarks>
///     By default the application category is enabled at the <see cref="Priority.Information" /> level, the assert
///     category is enabled at the <see cref="Priority.Warning" /> level, test is enabled at the
///     <see cref="Priority.Verbose" /> level and all other categories are enabled at the <see cref="Priority.Critical" />
///     level.
/// </remarks>
public enum Category
{
    /// <summary>The application category.</summary>
    Application,

    /// <summary>The error category.</summary>
    Error,

    /// <summary>The assert category.</summary>
    Assert,

    /// <summary>The system category.</summary>
    System,

    /// <summary>The audio category.</summary>
    Audio,

    /// <summary>The video category.</summary>
    Video,

    /// <summary>The render category.</summary>
    Render,

    /// <summary>The input category.</summary>
    Input,

    /// <summary>The test category.</summary>
    Test,

    /// <summary>A reserved category for future SDL library use.</summary>
    Reserved1,

    /// <summary>A reserved category for future SDL library use.</summary>
    Reserved2,

    /// <summary>A reserved category for future SDL library use.</summary>
    Reserved3,

    /// <summary>A reserved category for future SDL library use.</summary>
    Reserved4,

    /// <summary>A reserved category for future SDL library use.</summary>
    Reserved5,

    /// <summary>A reserved category for future SDL library use.</summary>
    Reserved6,

    /// <summary>A reserved category for future SDL library use.</summary>
    Reserved7,

    /// <summary>A reserved category for future SDL library use.</summary>
    Reserved8,

    /// <summary>A reserved category for future SDL library use.</summary>
    Reserved9,

    /// <summary>A reserved category for future SDL library use.</summary>
    Reserved10,

    /// <summary>Beyond this point is reserved for application use.</summary>
    /// <remarks>
    ///     For example:
    ///     <code>
    /// public enum MyPersonalCategory
    /// {
    ///     Category1 = Log.Category.Custom,
    ///     Category2,
    ///     Category3,
    ///     // ...
    /// }
    ///     </code>
    /// </remarks>
    Custom
}