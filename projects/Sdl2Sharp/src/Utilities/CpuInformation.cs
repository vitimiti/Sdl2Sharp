using Sdl2Sharp.Interop;

namespace Sdl2Sharp.Utilities;

/// <summary>A class to retrieve information about the CPU.</summary>
public static class CpuInformation
{
    /// <summary>Get an <see cref="int" /> with the number of logical CPU cores available.</summary>
    /// <remarks>
    ///     On CPUs that include technologies such as hyperthreading, the number of logical cores may be more than the
    ///     number of physical cores.
    /// </remarks>
    public static int LogicalCores => Sdl.GetCpuCount();

    /// <summary>Get an <see cref="int" /> with the L1 cache line size of the CPU.</summary>
    /// <remarks>This is useful for determining multi-threaded structure padding or SIMD prefetch sizes.</remarks>
    public static int CacheLineSize => Sdl.GetCpuCacheLineSize();

    /// <summary>Get an <see cref="int" /> with the amount of RAM configured in the system in MB.</summary>
    public static int SystemRam => Sdl.GetSystemRam();

    /// <summary>Determine whether the CPU has the RDTSC instruction.</summary>
    /// <remarks>This always returns <see langword="false" /> on CPUs that aren't using Intel instruction sets.</remarks>
    public static bool HasRdtsc => Sdl.HasRdtsc();

    /// <summary>Determine whether the CPU has AltiVec features.</summary>
    /// <remarks>This always returns <see langword="false" /> on CPUs that aren't using PowerPC instruction sets.</remarks>
    public static bool HasAltiVec => Sdl.HasAltiVec();

    /// <summary>Determine whether the CPU has MMX features.</summary>
    /// <remarks>This always returns <see langword="false" /> on CPUs that aren't using Intel instruction sets.</remarks>
    public static bool HasMmx => Sdl.HasMmx();

    /// <summary>Determine whether the CPU has 3DNow! features.</summary>
    /// <remarks>This always returns <see langword="false" /> on CPUs that aren't using AMD instruction sets.</remarks>
    public static bool Has3DNow => Sdl.Has3DNow();

    /// <summary>Determine whether the CPU has SSE features.</summary>
    /// <remarks>This always returns <see langword="false" /> on CPUs that aren't using Intel instruction sets.</remarks>
    public static bool HasSse => Sdl.HasSse();

    /// <summary>Determine whether the CPU has SSE2 features.</summary>
    /// <remarks>This always returns <see langword="false" /> on CPUs that aren't using Intel instruction sets.</remarks>
    public static bool HasSse2 => Sdl.HasSse2();

    /// <summary>Determine whether the CPU has SSE3 features.</summary>
    /// <remarks>This always returns <see langword="false" /> on CPUs that aren't using Intel instruction sets.</remarks>
    public static bool HasSse3 => Sdl.HasSse3();

    /// <summary>Determine whether the CPU has SSE4.1 features.</summary>
    /// <remarks>This always returns <see langword="false" /> on CPUs that aren't using Intel instruction sets.</remarks>
    public static bool HasSse41 => Sdl.HasSse41();

    /// <summary>Determine whether the CPU has SSE4.2 features.</summary>
    /// <remarks>This always returns <see langword="false" /> on CPUs that aren't using Intel instruction sets.</remarks>
    public static bool HasSse42 => Sdl.HasSse42();

    /// <summary>Determine whether the CPU has AVX features.</summary>
    /// <remarks>This always returns <see langword="false" /> on CPUs that aren't using Intel instruction sets.</remarks>
    public static bool HasAvx => Sdl.HasAvx();

    /// <summary>Determine whether the CPU has AVX2 features.</summary>
    /// <remarks>This always returns <see langword="false" /> on CPUs that aren't using Intel instruction sets.</remarks>
    public static bool HasAvx2 => Sdl.HasAvx2();

    /// <summary>Determine whether the CPU has AVX-512F (foundation) features.</summary>
    /// <remarks>This always returns <see langword="false" /> on CPUs that aren't using Intel instruction sets.</remarks>
    public static bool HasAvx512F => Sdl.HasAvx512F();

    /// <summary>Determine whether the CPU has ARM SIMD (ARMv6) features.</summary>
    /// <remarks>
    ///     <para>This is different from ARM NEON, which is a different instruction set.</para>
    ///     <para>This always returns <see langword="false" /> on CPUs that aren't using ARM instruction sets.</para>
    /// </remarks>
    public static bool HasArmSimd => Sdl.HasArmSimd();

    /// <summary>Determine whether the CPU has NEON (ARM SIMD) features.</summary>
    /// <remarks>This always returns <see langword="false" /> on CPUs that aren't using ARM instruction sets.</remarks>
    public static bool HasNeon => Sdl.HasNeon();
}