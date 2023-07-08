using Sdl2Sharp.Utilities;
using Sdl2Sharp.Utilities.Log;

Functions.SetLogPriority((int)MyCategories.LibraryInformation, Priority.Information);

Functions.SetLogOutputFunction((data, category, priority, message) =>
{
    DateTime dateTime = (DateTime)data;

    switch (priority)
    {
        case Priority.Verbose:
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            break;
        case Priority.Debug:
            Console.ForegroundColor = ConsoleColor.Green;
            break;
        case Priority.Information:
            Console.ForegroundColor = ConsoleColor.Blue;
            break;
        case Priority.Warning:
            Console.ForegroundColor = ConsoleColor.Yellow;
            break;
        case Priority.Error:
            Console.ForegroundColor = ConsoleColor.Red;
            break;
        case Priority.Critical:
            Console.ForegroundColor = ConsoleColor.DarkRed;
            break;
        case Priority.Total:
        default:
            throw new ArgumentOutOfRangeException(nameof(priority), priority, null);
    }

    Console.WriteLine(
        $"[{dateTime.Year:0000}/{dateTime.Month:00}/{dateTime.Day:00} - {dateTime.Hour:00}:{dateTime.Minute:00}:{dateTime.Second:00}] [{(MyCategories)category}] - {message}");

    Console.ResetColor();
}, DateTime.Now);

Functions.LogInformation((int)MyCategories.LibraryInformation,
    $"Running SDL v{NativeDllInformation.Version} [{NativeDllInformation.Revision}] on {Platform.Name}");

internal enum MyCategories
{
    LibraryInformation = Category.Custom
}