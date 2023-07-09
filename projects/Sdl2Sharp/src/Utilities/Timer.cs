using System.Runtime.InteropServices;

using Sdl2Sharp.CustomMarshalers;
using Sdl2Sharp.Exceptions;
using Sdl2Sharp.Interop;

namespace Sdl2Sharp.Utilities;

/// <summary>Timer and time related utilities.</summary>
/// <remarks>This class inherits from the <see cref="IDisposable" /> interface</remarks>
public sealed class Timer : IDisposable
{
    /// <summary>Function prototype for the timer callback function.</summary>
    /// <param name="interval">A <see cref="uint" /> with the timer interval.</param>
    /// <param name="parameter">An <see cref="object" /> with the timer callback parameter.</param>
    /// <returns>A <see cref="uint" /> with the next <paramref name="interval" />.</returns>
    /// <remarks>
    ///     <para>
    ///         The callback function is passed the current timer <paramref name="interval" /> and returns the next timer
    ///         interval. If the returned value is the same as the one passed in, the periodic alarm continues, otherwise a new
    ///         alarm is scheduled. If the callback returns 0, the periodic alarm is cancelled.
    ///     </para>
    ///     <para>
    ///         To pass in no <paramref name="parameter" /> (or a NULL/nullptr void pointer in C), you may use
    ///         <see cref="NullVoidPointer" />.
    ///     </para>
    /// </remarks>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint Callback(uint interval,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VoidPointerCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        object parameter);

    /// <summary>Call a callback function at a future time.</summary>
    /// <param name="interval">
    ///     A <see cref="uint" /> with the timer delay, in milliseconds, passed to
    ///     <paramref name="callback" />.
    /// </param>
    /// <param name="callback">
    ///     The <see cref="Callback" /> function to call when the specified <paramref name="interval" />
    ///     elapses.
    /// </param>
    /// <param name="parameter">An <see cref="object" /> that is passed to the <paramref name="callback" />.</param>
    /// <exception cref="TimerException">When SDL fails to create the timer.</exception>
    /// <remarks>
    ///     <para>
    ///         If you call this function, you must initialize <see cref="Subsystem" /> with
    ///         <see cref="SubsystemFlags.Timer" />.
    ///     </para>
    ///     <para>
    ///         The callback function is passed the current timer <paramref name="interval" /> and the user supplied
    ///         <paramref name="parameter" /> from the ctor call and should return the next timer interval. If the value
    ///         returned from the callback is 0, the timer is canceled.
    ///     </para>
    ///     <para>The callback is run on a separate thread.</para>
    ///     <para>
    ///         Timers take into account the amount of time it took to execute the callback. For example, if the callback
    ///         took 250ms to execute and returned 1000ms, the timer would only wait another 750ms before its next iteration.
    ///     </para>
    ///     <para>
    ///         Timing may be inexact due to OS scheduling. Be sure to note the current time with <see cref="Ticks" /> or
    ///         <see cref="PerformanceCounter" /> in case your callback needs to adjust for variances.
    ///     </para>
    /// </remarks>
    public Timer(uint interval, Callback callback, object parameter)
    {
        Id = Sdl.AddTimer(interval, callback, parameter);
        if (Id == 0)
        {
            throw new TimerException(Sdl.GetError());
        }
    }

    /// <summary>Get a <see cref="uint" /> with the number of milliseconds since SDL library initialization.</summary>
    /// <remarks>
    ///     <para>This value wraps if the program runs for more than ~49 days.</para>
    ///     <para>
    ///         This function is not recommended as of SDL 2.0.18; use <see cref="Ticks64" /> instead, where the value
    ///         doesn't wrap every ~49 days. There are places in SDL where we provide a 32-bit timestamp that can not change
    ///         without breaking binary compatibility, though, so this function isn't officially deprecated.
    ///     </para>
    /// </remarks>
    public static uint Ticks => Sdl.GetTicks();

    /// <summary>Get a <see cref="ulong" /> with the number of milliseconds since SDL library initialization.</summary>
    /// <remarks>
    ///     <para>
    ///         Note that you should not use the <see cref="TicksPassed" /> function with values returned by this function,
    ///         as that function does clever math to compensate for the 32-bit overflow every ~49 days that
    ///         <see cref="Ticks" /> suffers from. 64-bit values from this function can be safely compared directly.
    ///     </para>
    ///     <para>
    ///         For example, if you want to wait 100 ms, you could do this:
    ///         <code>
    /// const ulong timeOut = Timer.Ticks64 + 100;
    /// while (Timer.Ticks64 <![CDATA[<]]> timeOut)
    /// {
    ///     // Do work
    /// }
    ///         </code>
    ///     </para>
    /// </remarks>
    public static ulong Ticks64 => Sdl.GetTicks64();

    /// <summary>Get a <see cref="ulong" /> with the current value of the high resolution counter.</summary>
    /// <remarks>
    ///     <para>This function is typically used for profiling.</para>
    ///     <para>
    ///         The counter values are only meaningful relative to each other. Differences between values can be converted to
    ///         times by using <see cref="PerformanceFrequency" />.
    ///     </para>
    /// </remarks>
    public static ulong PerformanceCounter => Sdl.GetPerformanceCounter();

    /// <summary>Get a <see cref="ulong" /> the count per second of the high resolution counter.</summary>
    public static ulong PerformanceFrequency => Sdl.GetPerformanceFrequency();

    /// <summary>Get an <see cref="int" /> with the current timer ID.</summary>
    public int Id { get; }

    /// <summary>The public dispose function.</summary>
    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    ///     Compare 32-bit SDL ticks values, and return true if <paramref name="current" /> has passed
    ///     <paramref name="timeOut" />.
    /// </summary>
    /// <param name="current">A <see cref="uint" /> with the current 32-bit SDL ticks.</param>
    /// <param name="timeOut">A <see cref="uint" /> with the 32-bit SDL ticks to check that have been passed.</param>
    /// <returns><see langword="true" /> when <paramref name="current" /> has passed <paramref name="timeOut" />.</returns>
    /// <remarks>
    ///     <para>
    ///         This should be used with results from <see cref="Ticks" />, as this macro attempts to deal with the 32-bit
    ///         counter wrapping back to zero every ~49 days, but should _not_ be used with <see cref="Ticks64" />, which does
    ///         not have that problem.
    ///     </para>
    ///     <para>
    ///         For example, with <see cref="Ticks" />, if you want to wait 100 ms, you could do this:
    ///         <code>
    /// const uint timeOut = Timer.Ticks + 100;
    /// while (!TicksPassed(Ticks, timeOut))
    /// {
    ///     // Do work
    /// }
    ///         </code>
    ///     </para>
    /// </remarks>
    public static bool TicksPassed(uint current, uint timeOut)
    {
        return (int)(timeOut - current) <= 0;
    }

    /// <summary>Wait a specified number of milliseconds before returning.</summary>
    /// <param name="milliseconds">A <see cref="uint" /> with the milliseconds to delay.</param>
    /// <remarks>
    ///     This function waits a specified number of milliseconds before returning. It waits at least the specified time,
    ///     but possibly longer due to OS scheduling.
    /// </remarks>
    public static void Delay(uint milliseconds)
    {
        Sdl.Delay(milliseconds);
    }

    private void ReleaseUnmanagedResources()
    {
        _ = Sdl.RemoveTimer(Id);
    }

    /// <summary>The destructor.</summary>
    /// <remarks>Use <see cref="Dispose" /> instead.</remarks>
    ~Timer()
    {
        ReleaseUnmanagedResources();
    }
}