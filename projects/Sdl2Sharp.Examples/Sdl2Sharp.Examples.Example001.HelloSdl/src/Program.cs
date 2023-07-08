using Sdl2Sharp;
using Sdl2Sharp.Utilities;
using Sdl2Sharp.Utilities.Log;

Functions.SetLogPriority((int)MyCategories.LibraryInformation, Priority.Information);
Functions.SetLogPriority((int)MyCategories.SubsystemInformation, Priority.Information);
Functions.SetLogPriority((int)MyCategories.CpuInformation, Priority.Information);

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
    $"Using SDL v{NativeDllInformation.Version} [{NativeDllInformation.Revision}] on {Platform.Name}");

Functions.LogInformation((int)MyCategories.CpuInformation, $"Logical Cores: {CpuInformation.LogicalCores}");
Functions.LogInformation((int)MyCategories.CpuInformation, $"Cache Line Size: {CpuInformation.CacheLineSize}");
Functions.LogInformation((int)MyCategories.CpuInformation, $"System RAM: {CpuInformation.SystemRam}MB");
Functions.LogInformation((int)MyCategories.CpuInformation, $"CPU has the RDTSC instruction: {CpuInformation.HasRdtsc}");
Functions.LogInformation((int)MyCategories.CpuInformation, $"CPU has AltiVec features: {CpuInformation.HasAltiVec}");
Functions.LogInformation((int)MyCategories.CpuInformation, $"CPU has MMX features: {CpuInformation.HasMmx}");
Functions.LogInformation((int)MyCategories.CpuInformation, $"CPU has 3DNow! features: {CpuInformation.Has3DNow}");
Functions.LogInformation((int)MyCategories.CpuInformation, $"CPU has SSE features: {CpuInformation.HasSse}");
Functions.LogInformation((int)MyCategories.CpuInformation, $"CPU has SSE2 features: {CpuInformation.HasSse2}");
Functions.LogInformation((int)MyCategories.CpuInformation, $"CPU has SSE3 features: {CpuInformation.HasSse3}");
Functions.LogInformation((int)MyCategories.CpuInformation, $"CPU has SSE4.1 features: {CpuInformation.HasSse41}");
Functions.LogInformation((int)MyCategories.CpuInformation, $"CPU has SSE4.2 features: {CpuInformation.HasSse42}");
Functions.LogInformation((int)MyCategories.CpuInformation, $"CPU has AVX features: {CpuInformation.HasAvx}");
Functions.LogInformation((int)MyCategories.CpuInformation, $"CPU has AVX2 features: {CpuInformation.HasAvx2}");
Functions.LogInformation((int)MyCategories.CpuInformation, $"CPU has AVX-512F features: {CpuInformation.HasAvx512F}");
Functions.LogInformation((int)MyCategories.CpuInformation, $"CPU has ARM SIMD features: {CpuInformation.HasArmSimd}");
Functions.LogInformation((int)MyCategories.CpuInformation, $"CPU has NEON features: {CpuInformation.HasNeon}");

using (new Subsystem(SubsystemFlags.Video))
{
    Functions.LogInformation((int)MyCategories.SubsystemInformation,
        $"Running SDL with subsystems [{Subsystem.InitializedSubsystems}]");
}

internal enum MyCategories
{
    LibraryInformation = Category.Custom,
    SubsystemInformation,
    CpuInformation
}